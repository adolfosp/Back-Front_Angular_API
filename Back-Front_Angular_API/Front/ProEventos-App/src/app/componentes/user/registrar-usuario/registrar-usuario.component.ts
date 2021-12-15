import { AbstractControl, AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ValidacaoCampo } from '@app/helpers/ValidacaoCampo';

@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.scss']
})
export class RegistrarUsuarioComponent implements OnInit {

  form!: FormGroup;
  
  get f(): any{
    return this.form.controls;
  }

  constructor(private fb:FormBuilder) { }

  ngOnInit(): void {
    this.validacao()
  }

  public resetarForm(): void{
    this.form.reset();
  }

  private validacao(): void{
    const formOptions: AbstractControlOptions = {
      validators: ValidacaoCampo.MustMatch('senha','confirmeSenha')
    };

    this.form = this.fb.group({
      primeiroNome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      ultimoNome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(4), Validators.maxLength(12)]],
      usuario: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      senha: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      confirmeSenha: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]]


    }, formOptions);
  }
}
