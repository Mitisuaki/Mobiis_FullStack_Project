import { IItemSelecionado } from "./item-selecionado.interface";

export interface ILocalidadeFormValue {
  item: IItemSelecionado | null;
  tipo: number;
}
