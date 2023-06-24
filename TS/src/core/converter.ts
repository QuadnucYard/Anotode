import JSLoad = CS.Anotode.Utils.JSLoad;
const { $generic } = puer;

export function getEntries(obj: any) {
  const List = $generic(
    CS.System.Collections.Generic.List$1,
    JSLoad.JSObjectEntry
  );
  const entryList = new List();
  if (obj != null) {
    for (const [k, v] of Object.entries(obj)) {
      entryList.Add(new JSLoad.JSObjectEntry(k, v));
    }
  }
  return entryList;
}

/* export function toDict(obj: any) {
  const Dict = $generic(
    CS.System.Collections.Generic.Dictionary$2,
    CS.System.String,
    Object
  );
  const dict = new Dict();
  for (const [k, v] of Object.entries(obj)) {
    dict.Add(k, v);
  }
  return dict;
}
 */
