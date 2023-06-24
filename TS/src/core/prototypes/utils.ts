export function vec2(x: number, y: number) {
  return { x, y };
}

export function vec3(x: number, y: number, z: number) {
  return { x, y, z };
}

export function defineSprite(filename: string, ppu: number): SpriteData {
  return { filename, ppu };
}
