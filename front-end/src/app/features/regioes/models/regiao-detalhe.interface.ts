import { ICidade } from '../../../shared/models/cidade.interface';
import { IEstado } from '../../../shared/models/estado.interface';

export interface IRegiaoDetalhe {
  id: string;
  nome: string;
  ativo: boolean;
  cidades: ICidade[];
  estados: IEstado[];
}
