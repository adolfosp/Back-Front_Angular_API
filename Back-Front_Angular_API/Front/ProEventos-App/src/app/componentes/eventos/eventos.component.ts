import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../../models/Evento';
import { EventoService } from '../../services/Evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})

export class EventosComponent implements OnInit {
  modalRef?: BsModalRef;
  
  public eventos: Evento[] = [];
  public eventosFiltrados : Evento[] =[];
  public width: number = 100;
  public height: number = 100;
  public mostrarImagem: boolean = true;
  private _filtroListado: string = "";
  
  constructor(
              private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {}

  public get filtroLista(){
    return this._filtroListado
  }
 
  public set filtroLista(value: string){
     this._filtroListado = value;
     this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocalLowerCase().indexOf(filtrarPor) !== -1 || evento.local.toLocalLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  ngOnInit(): void {
    this.spinner.show();
    this.ObterEventos();
  }

  public AlterarEstadoDaImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  //Se inscrever em um observable gasta memória
  public ObterEventos(): void {
    this.eventoService.ObterEventos().subscribe({
      next: (eventos: Evento[]) =>{
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos
      },
      error: (error : any) => {
        this.spinner.hide();
        this.toastr.error("Erro ao carregar os Eventos!","Falha")
      } ,
      complete: ()  => this.spinner.hide()
      });
  }

  public AbrirModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
 
  public Confirmar(): void {
    this.toastr.success('Registro deletado com sucesso!', 'Deletado');
    this.modalRef?.hide();
  }
 
  public Recusar(): void {
    this.modalRef?.hide();
  }
  
}
