using System.Collections.Generic;

namespace Anotode.Simul {
	public class GameTimer {

		public const int framesPerSecond = 60;
		public const float fixedUpdateTime = 1.0f / framesPerSecond;

		public int time { get; set; }
		public int waveStartTime { get; set; }
		public int waveTime => time - waveStartTime;
		public int elapsed { get; set; }
		public float elapsedTime => elapsed * fixedUpdateTime;


		public void Update(int elapsed) {
			time += elapsed;
			this.elapsed = elapsed;
		}

		public Timer GetTimer(float interval) {
			return new Timer(this, interval);
		}

		public class Timer {
			public readonly GameTimer master;
			public readonly int startTime;
			public readonly float interval;
			private float _lastElapsed;

			public Timer(GameTimer master, float interval) {
				this.master = master;
				this.startTime = master.time;
				this.interval = interval;
				_lastElapsed = 0;
			}

			public void Reset(float warmup = 0.0f) {
				_lastElapsed = warmup;
			}

			public IEnumerable<float> CheckUpdate() {
				for (_lastElapsed += fixedUpdateTime; _lastElapsed >= interval; _lastElapsed -= interval) {
					yield return 0;
				}
			}
		}

	}
}
