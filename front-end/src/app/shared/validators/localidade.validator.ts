import { AbstractControl, FormArray, ValidationErrors, ValidatorFn } from '@angular/forms';
import { TipoLocalidadeEnum } from '../enums/tipo-localidade.enum';

export class LocalidadeValidators {

  public static semDuplicidades(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!(control instanceof FormArray)) return null;
      const ids = control.value.map((v: any) => v.item?.id).filter((id: string) => !!id);
      const duplicados = ids.filter((id: string, index: number) => ids.indexOf(id) !== index);
      return duplicados.length > 0 ? { duplicidadeDetectada: duplicados } : null;
    };
  }

  public static semConflitoCidadeEstado(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!(control instanceof FormArray)) return null;
      const valores = control.value;
      const estadosIds = valores
        .filter((v: any) => v.tipo === TipoLocalidadeEnum.Estado && v.item?.id)
        .map((v: any) => v.item.id);

      const cidadesConflito = valores
        .filter((v: any) => v.tipo === TipoLocalidadeEnum.Cidade && v.item?.estadoId && estadosIds.includes(v.item.estadoId))
        .map((v: any) => v.item.id);

      return cidadesConflito.length > 0 ? { conflitoDetectado: cidadesConflito } : null;
    };
  }
}
