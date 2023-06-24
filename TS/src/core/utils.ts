import Generic = CS.System.Collections.Generic;

const { $generic } = puer;

export function toList<T>(type: any, a: any[]) {
  const List = $generic(CS.System.Collections.Generic.List$1<T>, type);
  var list = new List();
  for (const x of a) {
    list.Add(Object.assign(new type(), x));
  }
  return list;
}

export function toNumberList<T>(type: any, a: number[]) {
  const List = $generic(Generic.List$1<T>, type);
  var list = new List();
  for (const x of a) {
    list.Add(x as T);
  }
  return list;
}

export function* iterate<T>(
  enumerable: Generic.IEnumerable$1<T>
): IterableIterator<T> {
  for (const i = enumerable.GetEnumerator(); i.MoveNext(); ) {
    yield i.Current;
  }
}
