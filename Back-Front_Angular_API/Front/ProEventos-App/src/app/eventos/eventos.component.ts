import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/Evento.service';

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

  constructor(private eventoService: EventoService, private modalService: BsModalService) {

  }

  ngOnInit(): void {
    this.ObterEventos()
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
      error: (error : any) => console.error(error)  
      });
  }

  public AbrirModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
 
  public Confirmar(): void {
    
    this.modalRef?.hide();
  }
 
  public Recusar(): void {
    this.modalRef?.hide();
  }
}
