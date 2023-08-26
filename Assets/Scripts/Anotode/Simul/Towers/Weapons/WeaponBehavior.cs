using System;
using System.Collections.Generic;
using Anotode.Models.Towers.Projectiles;
using Anotode.Simul.Enemies;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers.Emissons;
using Anotode.Simul.Towers.Projectiles;
using Quadnuc.Utils;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Towers.Weapons {
	public class WeaponBehavior : Simulatable {
		public Weapon weapon;

		/// <summary><code>public virtual unsafe bool CanFire()</code></summary>
		public Func<bool> canFire = FuncTools.Tautology;

		/// <summary><code>public virtual unsafe float GetRate(float rate)</code></summary>
		public Func<float, float> getRate = FuncTools.Identity;

		/// <summary><code> public virtual unsafe bool OnPreEmissionCheck()</code></summary>
		//public Func<bool> onPreEmissionCheck = FuncTools.Tautology;

		/// <summary><code>public virtual unsafe bool FilterEmission()</code></summary>
		public Func<bool> filterEmission = FuncTools.Tautology;

		/// <summary><code>public virtual Emission GetEmission(Emission currentEmissionDef, bool doubleShot = false)</code></summary>
		public Func<Emission, Emission> getEmission = FuncTools.Identity;

		/// <summary><code>public virtual Optional<float> EmitRotation()</code></summary>
		public Func<float?> getEmitRotation = FuncTools.Default<float?>;

		/// <summary><code>public virtual Optional<Vector3> EmitPosition()</code></summary>
		public Func<Vector3?> getEmitPosition = FuncTools.Default<Vector3?>;

		/// <summary><code>public virtual void EmissionStarted()</code></summary>
		public Action onEmissionStart = FuncTools.NoAction;

		/// <summary><code>public virtual ProjectileModel GetProjectile(ProjectileModel currentProjectileDef, bool doubleShot = false)</code></summary>
		public Func<ProjectileModel, ProjectileModel> getProjectile = FuncTools.Identity;

		/// <summary><code>public virtual void ProjectilesCreated(SizedList<Projectile> projectiles)</code></summary>
		public Action<List<Projectile>> onProjectilesCreated = FuncTools.NoAction;

		/// <summary><code>public virtual void OnBloonDamaged(Bloon bloon, float amount)</code></summary>
		public Action<Enemy, float> onEnemyDamaged = FuncTools.NoAction;

	}
}
