type int = number;

interface vector2 {
  x: number;
  y: number;
}

interface vector3 {
  x: number;
  y: number;
  z: number;
}

interface SpriteData {
  filename: string;
  ppu: number;
}

type Type = new (...args: readonly any[]) => unknown;

interface BehaviorModel {
  $base?: Type;
  $impl: Type;
}