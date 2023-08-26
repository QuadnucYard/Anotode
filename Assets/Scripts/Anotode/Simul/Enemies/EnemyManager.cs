using System;
using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Towers;
using Anotode.Simul.Objects;
using Quadnuc.Utils;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Enemies {
	public class EnemyManager : Simulatable {

		public event Action<Enemy> onEnemySpawned;

		public delegate void EnemyInvadeDelegate(Enemy enemy, int livesBefore, int livesAfter);
		public event EnemyInvadeDelegate onEnemyInvade;

		public delegate void EnemyAreaChangedDelegate(Enemy enemy, ObjectId areaBefore, ObjectId areaAfter);

		public event EnemyAreaChangedDelegate onEnemyAreaChanged;


		public void EnemySpawned(Enemy enemy) {
			onEnemySpawned?.Invoke(enemy);
		}

		public void EnemyInvade(Enemy enemy, int livesBefore, int livesAfter) {
			onEnemyInvade?.Invoke(enemy, livesBefore, livesAfter);
		}

		public void EnemyAreaChanged(Enemy enemy, ObjectId areaBefore, ObjectId areaAfter) {
			enemy.areaIdIn = areaAfter;
			sim.map.areaEnemies[areaBefore].Remove(enemy);
			sim.map.areaEnemies[areaAfter].Add(enemy);
			onEnemyAreaChanged?.Invoke(enemy, areaBefore, areaAfter);
		}

		public delegate bool EnemyFilter(Enemy enemy);

		public Enemy GetEnemyById(ObjectId id) {
			return GetEnemies().First(t => t.id == id);
		}

		public IEnumerable<Enemy> GetEnemies() => sim.map.areaEnemies.Values.SelectMany(t => t);

		public Enemy GetTarget(Vector2 position, float range, TargetType targetType, EnemyFilter filter = null) {
			return GetTargets(position, range, targetType, 1, filter).FirstOrDefault();
		}

		public IEnumerable<Enemy> GetTargets(Vector2 position, float range, TargetType targetType, int count, EnemyFilter filter = null) {
			if (targetType == TargetType.None) {
				return Enumerable.Empty<Enemy>();
			}
			var enemies = GetEnemies().Where(e => Vector2.Distance(e.mapPos, position) < range);
			if (filter != null) {
				enemies = enemies.Where(e => filter(e));
			}
			enemies = targetType switch {
				TargetType.First => enemies.OrderByDescending(e => e.move.distanceTraveled),
				TargetType.Last => enemies.OrderBy(e => e.move.distanceTraveled),
				TargetType.Close => enemies.OrderBy(e => Vector2.Distance(e.mapPos, position)),
				TargetType.Far => enemies.OrderByDescending(e => Vector2.Distance(e.mapPos, position)),
				TargetType.Strong => enemies.OrderByDescending(e => (float)e.enemyModel.hp / e.enemyModel.hpMax),
				TargetType.Weak => enemies.OrderBy(e => (float)e.enemyModel.hp / e.enemyModel.hpMax),
				TargetType.Random => enemies,
				TargetType.Any => enemies,
				_ => throw new NotImplementedException(),
			};
			return enemies.Take(count);
		}

	}
}
