import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.Validacao();
  }

  public Validacao() : void{
    this.form = new FormGroup({
      local: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      dataEvento: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      quantidadePessoas: new FormControl('', Validators.max(120.000)),
      imagemURL: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      telefone: new FormControl(),
      email: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)])

    });
  }
}
