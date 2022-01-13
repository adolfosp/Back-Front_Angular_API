import { Lote } from '@app/models/Lote';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

import { Evento } from './../../../models/Evento';
import { EventoService } from '../../../services/evento.service';

import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  evento = {} as Evento
  form!: FormGroup;
  estadoSalvar: string = 'post';

  get modoEditar(): boolean{
    return this.estadoSalvar === 'put';
  }
  
  get f(): any{
    return this.form.controls;
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray
  }
  //propriedade
  get bsConfig() : any{
    return { 
        adaptivePosition: true,
        dateInputFormat: 'DD/MM/YYYY hh:mm a',
        containerClass: 'theme-default',
        showWeekNumbers: false
      }
  }

  constructor(
    private fb:FormBuilder,
    private localeService: BsLocaleService,
    private activateRouter: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toaster: ToastrService,
    private router: Router)
    {
      this.localeService.use('pt-br')
    }

  ngOnInit(): void {
    this.carregarEvento();
    this.validacao();
  }

  public carregarEvento() : void{
    const eventoIdParametro = this.activateRouter.snapshot.paramMap.get('id');

    if(eventoIdParametro !== null){

      this.estadoSalvar = 'put';
      this.spinner.show();

      this.eventoService.ObterEventoById(+eventoIdParametro).subscribe(
        {
          next: (evento: Evento) => {
            this.evento = {...evento};
            this.form.patchValue(this.evento);
          },
          error: (error: any) => {
            this.spinner.hide(),
            this.toaster.error('Erro ao tentar carregar o evento', 'Erro!')
            console.error(error)
          },
          complete: () => {
            this.spinner.hide()
          },
        }
      )
    }
  }

  public resetarForm(): void{
    this.form.reset();
  }

  private validacao() : void{
    this.form = this.fb.group({
      local: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      dataEvento: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      quantidadePessoas: ['', Validators.max(120.000)],
      imagemURL: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      telefone: [],
      email: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      lotes: this.fb.array([])

    });
  }

  public adicionarLote(): void {
    (this.lotes).push(
        this.criarLote({id: 0} as Lote)
    );
  }

  private criarLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim],
      quantidade: [lote.quantidade, Validators.required],
      
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched}
  }

  public salvarAlteracao(): void{
    this.spinner.show();
    if(this.form.valid){

      this.evento = (this.estadoSalvar === 'post') ? {...this.form.value} : {id: this.evento.id, ...this.form.value};

      this.eventoService[this.estadoSalvar](this.evento).subscribe(
        (eventoRetorno: Evento) => {
          this.toaster.success('Evento salvo com sucesso', 'Sucesso!');
          this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);

        },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toaster.error(`Não foi possível salvar o evento. Mensagem: ${error}`, 'Erro!')
        },
        () => this.spinner.hide()
      );
    }
  }
}
