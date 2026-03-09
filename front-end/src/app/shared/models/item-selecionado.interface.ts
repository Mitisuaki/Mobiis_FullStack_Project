import { TipoLocalidadeEnum } from '../enums/tipo-localidade.enum';

export interface IItemSelecionado {
  id: string;
  nome: string;
  tipo: TipoLocalidadeEnum;
  estadoId?: string;
}
