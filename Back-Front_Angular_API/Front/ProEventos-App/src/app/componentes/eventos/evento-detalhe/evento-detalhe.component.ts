import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Lote } from '@app/models/Lote';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

import { Evento } from './../../../models/Evento';
import { EventoService } from '../../../services/evento.service';

import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Event, Router } from '@angular/router';
import { LoteService } from '@app/services/lote.service';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  eventoId: number;
  evento = {} as Evento
  form!: FormGroup;
  estadoSalvar: string = 'post';
  loteAtual = {id: 0, nome: '', indice: 0};
  imagemURL = 'assets/img/upload.png'

  modalRef: BsModalRef;

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
    private router: Router,
    private loteService: LoteService,
    private modalService: BsModalService)
    {
      this.localeService.use('pt-br')
    }

  ngOnInit(): void {
    this.carregarEvento();
    this.validacao();
  }

  public carregarEvento() : void{
   this.eventoId = +this.activateRouter.snapshot.paramMap.get('id');

    if(this.eventoId !== null && this.eventoId !== 0){

      this.estadoSalvar = 'put';
      this.spinner.show();

      this.eventoService.ObterEventoById(this.eventoId).subscribe(
        {
          next: (evento: Evento) => {
            this.evento = {...evento};
            this.form.patchValue(this.evento);
            this.evento.lotes.forEach(lote => {
              this.lotes.push(this.criarLote(lote));
            });
          
          },
          error: (error: any) => {
            this.toaster.error('Erro ao tentar carregar o evento', 'Erro!');
            console.error(error);
          }
        }
      ).add(() => this.spinner.hide());
    }
  }

  public mudarValorData(value: Date, indice: number, campo: string): void{
    this.lotes.value[indice][campo] = value;
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

  public salvarEvento(): void{
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

  public salvarLotes(): void {

    if(this.form.controls.lotes.valid){
      this.spinner.show();

      this.loteService.salvarLote(this.eventoId, this.form.value.lotes)
          .subscribe(
            () => {
              this.toaster.success('Lotes salvos com sucesso!', 'Sucesso');
              this.lotes.reset();
            },
            (error: any) => {
              this.toaster.error('Erro ao tentar salvar lotes!', 'Erro');
              console.error(error);
            },
            () => {}
          ).add(() => this.spinner.hide());
    }
  }

  public removerLote(template: TemplateRef<any>, indice: number ): void{

    this.loteAtual.id = this.lotes.get(indice + '.id').value;
    this.loteAtual.nome = this.lotes.get(indice + '.nome').value;
    this.loteAtual.indice = indice;

     this.modalRef = this.modalService.show(template, {class: 'modal-sm' });
  }

  public confirmar(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.loteService.deletarLote(this.eventoId, this.loteAtual.id).subscribe(
      () =>{
        this.toaster.success('Lote removido com sucesso!', 'Sucesso');
        this.lotes.removeAt(this.loteAtual.indice); 
      },
      (error: any) =>{
        this.toaster.error(`Erro ao deletar lote ${this.loteAtual.id}!`,'Erro');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  public recusar(): void {
    this.modalRef.hide();
  }

  public retornarTituloLote(nomeLote: string): string {
    return nomeLote === null || nomeLote=== ''? 'Nome do Lote' :nomeLote
  }
}
