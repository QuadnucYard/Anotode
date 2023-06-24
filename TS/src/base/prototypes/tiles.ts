const { TileType } = CS.Anotode.Models.Map;

const createTileData = (
  index: number,
  id: string,
  type: CS.Anotode.Models.Map.TileType
) => ({
  id,
  index,
  type,
  sprite: {
    filename: `__base__/graphics/map/tiles/${id}.png`,
    ppu: 32,
  },
});

const data: TileData[] = [
  createTileData(0, "tile0", TileType.Void),
  createTileData(1, "tile1", TileType.Land),
  createTileData(2, "tile2", TileType.Land),
  createTileData(3, "tile3", TileType.Land),
  createTileData(4, "tile4", TileType.Land),
  createTileData(5, "tile5", TileType.Land),
  createTileData(6, "tile6", TileType.Land),
  createTileData(7, "tile7", TileType.Land),
  createTileData(8, "tile8", TileType.Land),
  createTileData(9, "tile9", TileType.Land),
  createTileData(10, "tile10", TileType.Land),
  createTileData(11, "tile11", TileType.Land),
  createTileData(12, "tile12", TileType.Land),
];

export default data;
