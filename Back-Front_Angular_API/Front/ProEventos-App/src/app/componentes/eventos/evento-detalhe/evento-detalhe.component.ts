import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;
  get f(): any{
    return this.form.controls;
  }

  constructor(private fb:FormBuilder) { }

  ngOnInit(): void {
    this.Validacao();
  }

  public ResetarForm(): void{
    this.form.reset();
  }

  public Validacao() : void{
    this.form = this.fb.group({
      local: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      dataEvento: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      quantidadePessoas: ['', Validators.max(120.000)],
      imagemURL: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      telefone: [],
      email: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]

    });
  }
}
