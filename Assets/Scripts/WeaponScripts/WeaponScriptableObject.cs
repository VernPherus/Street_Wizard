using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Pool;
using WeaponsScripts.Damage;
using WeaponsScripts.ImpactEffects;


namespace WeaponsScripts
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponScriptableObject")]
    public class WeaponScriptableObject : ScriptableObject, System.ICloneable
    {
        public string weaponName;

        public ImpactType ImpactType;
        public WeaponType weaponType;
        public GameObject ModelPrefab;

        public Vector3 SpawnPoint;
        public Vector3 SpawnRotation;

        public DamageConfig DamageConfig;
        public AmmoConfig AmmoConfig;
        public ShootConfig ShootConfig;
        public WeaponTrail WeaponTrail;

        public ICollisionHandler[] BulletImpactEffects = new ICollisionHandler[0];

        private GameObject Model;
        private Camera ActiveCamera;

        private float LastShootTime;
        private float InitialClickTime;
        private float StopShootTime;
        private bool LastFrameWantedToShoot;

        private ParticleSystem ShootSystem;
        private ObjectPool<Projectile> ProjectilePool;
        private ObjectPool<TrailRenderer> TrailPool;

        private MonoBehaviour ActiveMonoBehavior;

        private void UpdateCamera(Camera ActiveCamera)
        {
            this.ActiveCamera = ActiveCamera;
        }

        // #############################################################################################################
        //* ## Spawn function ##        
        // #############################################################################################################

        public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehavior, Camera ActiveCamera = null)
        {

            this.ActiveMonoBehavior = ActiveMonoBehavior;
            this.ActiveCamera = ActiveCamera;
            LastShootTime = 0;
            TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);

            if (ShootConfig.firingType == FiringType.projectile)
            {
                ProjectilePool = new ObjectPool<Projectile>(CreateProjectile);
            }

            Model = Instantiate(ModelPrefab);
            Model.transform.SetParent(Parent, false);
            Model.transform.SetLocalPositionAndRotation(SpawnPoint, Quaternion.Euler(SpawnRotation));

            ShootSystem = Model.GetComponentInChildren<ParticleSystem>();
        }

        /// <summary>
        /// Despawns the active gameobjects and cleans up pools.
        /// </summary>
        public void Despawn()
        {
            // We do a bunch of other stuff on the same frame, so we really want it to be immediately destroyed, not at Unity's convenience.
            Model.SetActive(false);
            Destroy(Model);
            TrailPool.Clear();
            if (ProjectilePool != null)
            {
                ProjectilePool.Clear();
            }

            ShootSystem = null;
        }

        // #############################################################################################################
        //* ## Tick logic ##        
        // #############################################################################################################

        public void Tick(bool WantsToShoot)
        {
            Model.transform.localRotation = Quaternion.Lerp(
                Model.transform.localRotation,
                Quaternion.Euler(SpawnRotation),
                Time.deltaTime * ShootConfig.RecoilRecoverySpeed
            );

            if (WantsToShoot)
            {
                LastFrameWantedToShoot = true;
                if (AmmoConfig.CurrentAmmo > 0)
                {
                    TryToShoot();
                }

            }
            if (!WantsToShoot && LastFrameWantedToShoot)
            {
                StopShootTime = Time.time;
                LastFrameWantedToShoot = false;
            }
        }

        // #############################################################################################################
        //* ## Weapon Effects ##        
        // #############################################################################################################

        private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 Endpoint, RaycastHit Hit)
        {
            TrailRenderer instance = TrailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = StartPoint;
            yield return null;

            instance.emitting = true;

            float distance = Vector3.Distance(StartPoint, Endpoint);
            float remainingDistance = distance;
            while (remainingDistance > 0)
            {
                instance.transform.position = Vector3.Lerp(
                    StartPoint,
                    Endpoint,
                    Mathf.Clamp01(1 - (remainingDistance / distance))

                );

                remainingDistance -= WeaponTrail.SimulationSpeed * Time.deltaTime;

                yield return null;
            }

            instance.transform.position = Endpoint;


            if (Hit.collider != null)
            {
                HandleBulletImpact(distance, Endpoint, Hit.normal, Hit.collider);

                // TODO: Add surface manager for hit effects
                //SurfaceManager.Instance.HandleImpact(
                //Hit.transform.gameObject,
                //Endpoint, 
                //Hit.normal,
                //ImpactType,
                //0
                //)

                // if (Hit.collider.TryGetComponent(out IDamageable damageable))
                // {
                //     damageable.TakeDamage(DamageConfig.GetDamage(distance));
                // }
            }

            yield return new WaitForSeconds(WeaponTrail.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            TrailPool.Release(instance);
        }

        private IEnumerator DelayedDisableTrail(TrailRenderer trail)
        {
            yield return new WaitForSeconds(WeaponTrail.Duration);
            yield return null;
            trail.emitting = false;
            trail.gameObject.SetActive(false);
            TrailPool.Release(trail);
        }

        private TrailRenderer CreateTrail()
        {
            GameObject instance = new GameObject("Bullet Trail");
            TrailRenderer trail = instance.AddComponent<TrailRenderer>();
            trail.colorGradient = WeaponTrail.Color;
            trail.material = WeaponTrail.material;
            trail.widthCurve = WeaponTrail.widthCurve;
            trail.time = WeaponTrail.Duration;
            trail.minVertexDistance = WeaponTrail.MinVertexDistance;

            trail.emitting = false;
            trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            return trail;
        }

        // #############################################################################################################
        //* ## Shoot logic ##        
        // #############################################################################################################

        /*
            * <Summary>
            - Perform shooting raycast
            - Applies bullet spread and plays sound effects(Soon to be added)
            - Ammo consumption
        */
        public virtual void TryToShoot()
        {
            if (Time.time - LastShootTime - ShootConfig.FireRate > Time.deltaTime)
            {
                float lastDuration = Mathf.Clamp(
                    0,
                    StopShootTime - InitialClickTime,
                    ShootConfig.MaxSpreadTime
                );
                float lerptTime = (ShootConfig.RecoilRecoverySpeed - (Time.time - StopShootTime)) / ShootConfig.RecoilRecoverySpeed;

                InitialClickTime = Time.time - Mathf.Lerp(0, lastDuration, Mathf.Clamp01(lerptTime)); // keep recoil after shooting
            }


            if (Time.time > ShootConfig.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;
                if (AmmoConfig.CurrentAmmo == 0)
                {
                    Debug.Log("Out of Ammo!");
                    return;
                }

                ShootSystem.Play();

                AmmoConfig.CurrentAmmo--;

                for (int i = 0; i < ShootConfig.BulletsPerShot; i++)
                {
                    Vector3 spreadAmount = ShootConfig.GetSpread(Time.time - InitialClickTime);

                    Vector3 shootDirection = Vector3.zero;
                    Model.transform.forward += Model.transform.TransformDirection(spreadAmount);
                    if (ShootConfig.AimType == AimType.FromWeapon)
                    {
                        shootDirection = ShootSystem.transform.forward;
                    }
                    else
                    {
                        shootDirection = ActiveCamera.transform.forward
                            + ActiveCamera.transform.TransformDirection(shootDirection);
                    }

                    if (ShootConfig.firingType == FiringType.hitscan)
                    {
                        HitscanShoot(shootDirection);
                    }
                    else if (ShootConfig.firingType == FiringType.projectile)
                    {
                        ProjectileShoot(shootDirection);
                    }
                    else
                    {
                        BeamShoot();
                    }
                }

            }
        }

        public Vector3 GetRaycastOrigin()
        {
            Vector3 origin = ShootSystem.transform.position;

            if (ShootConfig.AimType == AimType.FromCamera)
            {
                origin = ActiveCamera.transform.position
                    + ActiveCamera.transform.forward
                        * Vector3.Distance(
                            ActiveCamera.transform.position,
                            ShootSystem.transform.position
                        );
            }

            return origin;
        }

        public Vector3 GetGunForward()
        {
            return Model.transform.forward;
        }

        public bool CanReplenish()
        {
            return AmmoConfig.CanReplenish();
        }

        // #############################################################################################################
        //* ## Hitscan logic ##        
        // #############################################################################################################

        private void HitscanShoot(Vector3 shootDirection)
        {
            if (Physics.Raycast(
                    GetRaycastOrigin(),
                    shootDirection,
                    out RaycastHit hit,
                    float.MaxValue,
                    ShootConfig.Hitmask
                ))
            {
                ActiveMonoBehavior.StartCoroutine(
                    PlayTrail(
                        ShootSystem.transform.position,
                        hit.point,
                        hit
                    )
                );
            }
            else
            {
                ActiveMonoBehavior.StartCoroutine(
                    PlayTrail(
                        ShootSystem.transform.position,
                        ShootSystem.transform.position + (shootDirection * WeaponTrail.MissDistance),
                        new RaycastHit()
                    )
                );
            }

        }

        // #############################################################################################################
        // * ## Projectile Logic ##
        // #############################################################################################################

        private void ProjectileShoot(Vector3 ShootDirection)
        {
            Projectile projectile = ProjectilePool.Get();
            projectile.gameObject.SetActive(true);
            projectile.OnCollision += HandleProjectileCollision;

            if (ShootConfig.AimType == AimType.FromCamera
                && Physics.Raycast(
                        GetRaycastOrigin(),
                        ShootDirection,
                        out RaycastHit hit,
                        float.MaxValue,
                        ShootConfig.Hitmask)
            )
            {
                Vector3 directionToHit = (hit.point - ShootSystem.transform.position).normalized;
                Model.transform.forward = directionToHit;
                ShootDirection = directionToHit;
            }

            projectile.transform.position = ShootSystem.transform.position;
            projectile.Spawn(ShootDirection * ShootConfig.BulletSpawnForce);

            TrailRenderer trail = TrailPool.Get();
            if (trail != null)
            {
                trail.transform.SetParent(projectile.transform, false);
                trail.transform.localPosition = Vector3.zero;
                trail.emitting = true;
                trail.gameObject.SetActive(true);
            }
        }

        private void HandleProjectileCollision(Projectile projectile, Collision collision)
        {
            TrailRenderer trail = projectile.GetComponentInChildren<TrailRenderer>();

            if (trail != null)
            {
                trail.transform.SetParent(null, true);
                ActiveMonoBehavior.StartCoroutine(DelayedDisableTrail(trail));
            }

            projectile.gameObject.SetActive(false);
            ProjectilePool.Release(projectile);

            if (collision != null)
            {
                ContactPoint contactPoint = collision.GetContact(0);

                HandleBulletImpact(
                    Vector3.Distance(contactPoint.point, projectile.SpawnLocation),
                    contactPoint.point,
                    contactPoint.normal,
                    contactPoint.otherCollider
                );
            }
        }

        private void HandleBulletImpact(
            float DistanceTraveled,
            Vector3 HitLocation,
            Vector3 HitNormal,
            Collider HitCollider)
        {
            // Implement surface manager here
            SurfaceManager.Instance.HandleImpact(HitCollider.gameObject, HitLocation, HitNormal, ImpactType, 0);

            if (HitCollider.TryGetComponent(out IDamageable damageable))
            {
                // Implement damage calculator functionality here
                damageable.TakeDamage(DamageConfig.GetDamage(DistanceTraveled), DamageConfig.DamageType);
            }

            foreach (ICollisionHandler handler in BulletImpactEffects)
            {
                handler.HandleImpact(HitCollider, HitLocation, HitNormal, this);
            }
        }

        // #############################################################################################################
        // * ## Beam Logic ##
        // #############################################################################################################

        private void BeamShoot()
        {

        }

        // #############################################################################################################
        // * ## Projectile Logic ## 
        // #############################################################################################################

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(ShootConfig.projectilePrefab);
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.mass = ShootConfig.BulletWeight;

            return Instantiate(projectile);
        }

        public object Clone()
        {
            WeaponScriptableObject config = CreateInstance<WeaponScriptableObject>();

            config.weaponType = weaponType;
            config.name = name;
            config.weaponName = weaponName;
            config.DamageConfig = DamageConfig.Clone() as DamageConfig;
            config.ShootConfig = ShootConfig.Clone() as ShootConfig;
            config.AmmoConfig = AmmoConfig.Clone() as AmmoConfig;
            config.WeaponTrail = WeaponTrail.Clone() as WeaponTrail;


            config.ModelPrefab = ModelPrefab;
            config.SpawnPoint = SpawnPoint;
            config.SpawnRotation = SpawnRotation;

            return config;
        }
    }
}

