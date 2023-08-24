import { def } from "../../core/def";
import ue = CS.UnityEngine;

import { vec2 } from "../../core/prototypes/utils";
import { AreaMove, AreaMoveModel, LadderMove } from "./area-behaviors";

const data: MapData[] = [
  {
    id: "map-0-0-0",
    areas: [
      {
        tiles: [
          [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
          [1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 1],
          [1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1],
          [1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1],
          [1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0],
          [1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1],
          [1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1],
          [1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1],
          [1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1],
          [1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1], // 15*10
        ],
        entrances: [vec2(0, 0), vec2(0, 9)],
        exits: [vec2(14, 9)],
        pivotPoint: vec2(0, 0),
        position: vec2(0, 0),
      },
      {
        tiles: [
          [1, 1, 1, 1, 1],
          [1, 0, 1, 0, 1],
          [1, 1, 1, 1, 1],
          [1, 0, 1, 0, 1],
          [1, 1, 1, 1, 1], // 5*5
        ],
        exits: [vec2(2, 2)],
        pivotPoint: vec2(0, 0),
        position: vec2(16, 0),
      },
    ],
  },
  {
    id: "map-0-0-1",
    areas: [
      {
        tiles: [
          [1, 1, 1, 1, 1],
          [1, 1, 2, 1, 1],
          [1, 2, 2, 2, 1],
          [1, 1, 2, 1, 1],
          [1, 1, 1, 1, 1],
        ],
        entrances: [vec2(0, 1)],
        pivotPoint: vec2(0, 0),
        position: vec2(0, 0),
      },
      {
        tiles: [[2, 2, 2]],
        pivotPoint: vec2(0, 0),
        position: vec2(5, 1),
        behaviors: [
          def<AreaMoveModel>({
            $impl: AreaMove,
            motionFunc(t) {
              return new ue.Vector2(0, Math.sin(t) * 0.5);
            },
          }),
        ],
      },
      {
        tiles: [[2, 2, 2]],
        pivotPoint: vec2(0, 0),
        position: vec2(5, 3),
        behaviors: [
          def<AreaMoveModel>({
            $impl: AreaMove,
            motionFunc(t) {
              return new ue.Vector2(0, Math.cos(t) * 0.5);
            },
          }),
        ],
      },
      {
        tiles: [
          [1, 1, 1],
          [1, 1, 1],
          [1, 1, 1],
        ],
        exits: [vec2(2, 1)],
        pivotPoint: vec2(0, 0),
        position: vec2(8, 1),
        behaviors: [
          def<AreaMoveModel>({
            $impl: AreaMove,
            motionFunc(t) {
              t = (t / 2) % 4;
              const d = t < 1 ? 1 : t < 2 ? 3 - 2 * t : t < 3 ? -1 : 2 * t - 7;
              return new ue.Vector2(0, d * 2);
            },
          }),
        ],
      },
    ],
  },
  {
    id: "map-0-0-2",
    areas: [
      {
        tiles: [
          [10, 11, 10],
          [11, 10, 11],
          [10, 0, 10],
          [11, 10, 11],
          [10, 11, 10],
        ],
        entrances: [vec2(0, 2)],
        pivotPoint: vec2(0, 0),
        position: vec2(0, 2),
        behaviors: [Object.assign({}, LadderMove, { amplitude: 2, vertical: true })],
      },
      {
        tiles: [[12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12]],
        pivotPoint: vec2(0, 0),
        position: vec2(3, 0),
      },
      {
        tiles: [[12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12]],
        pivotPoint: vec2(0, 0),
        position: vec2(3, 8),
      },
      {
        tiles: [[3, 3, 3, 3, 3, 3, 3]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 1),
        behaviors: [Object.assign({}, LadderMove, { phase: 0 / 7 / 3 })],
      },
      {
        tiles: [[4, 4, 4, 4, 4, 4, 4]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 2),
        behaviors: [Object.assign({}, LadderMove, { phase: 1 / 7 / 3 })],
      },
      {
        tiles: [[5, 5, 5, 5, 5, 5, 5]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 3),
        behaviors: [Object.assign({}, LadderMove, { phase: 2 / 7 / 3 })],
      },
      {
        tiles: [[6, 6, 6, 6, 6, 6, 6]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 4),
        behaviors: [Object.assign({}, LadderMove, { phase: 3 / 7 / 3 })],
      },
      {
        tiles: [[7, 7, 7, 7, 7, 7, 7]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 5),
        behaviors: [Object.assign({}, LadderMove, { phase: 4 / 7 / 3 })],
      },
      {
        tiles: [[2, 2, 2, 2, 2, 2, 2]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 6),
        behaviors: [Object.assign({}, LadderMove, { phase: 5 / 7 / 3 })],
      },
      {
        tiles: [[1, 1, 1, 1, 1, 1, 1]],
        pivotPoint: vec2(3, 0),
        position: vec2(9, 7),
        behaviors: [Object.assign({}, LadderMove, { phase: 6 / 7 / 3 })],
      },
      {
        tiles: [
          [8, 9],
          [9, 8],
          [8, 9],
        ],
        pivotPoint: vec2(0, 0),
        position: vec2(16, 1),
        behaviors: [Object.assign({}, LadderMove, { b: 1, amplitude: 1, vertical: true })],
      },
      {
        tiles: [
          [8, 9],
          [9, 8],
          [8, 9],
        ],
        pivotPoint: vec2(0, 0),
        position: vec2(16, 5),
        behaviors: [Object.assign({}, LadderMove, { b: 1, amplitude: 1, vertical: true })],
      },
      {
        tiles: [
          [9, 8],
          [8, 9],
          [9, 8],
        ],
        pivotPoint: vec2(0, 1),
        position: vec2(18, 4),
        behaviors: [
          def<AreaMoveModel>({
            $impl: AreaMove,
            motionFunc(t) {
              return new ue.Vector2(0, Math.sin(t) * 3);
            },
          }),
        ],
      },
      {
        tiles: [[10], [11]],
        exits: [vec2(0, 1)],
        pivotPoint: vec2(0, 0),
        position: vec2(18, -1),
      },
      {
        tiles: [[10]],
        exits: [vec2(0, 0)],
        pivotPoint: vec2(0, 0),
        position: vec2(18, 4),
      },
      {
        tiles: [[10], [11]],
        exits: [vec2(0, 0)],
        pivotPoint: vec2(0, 0),
        position: vec2(18, 8),
      },
    ],
  },
];

export default data;
