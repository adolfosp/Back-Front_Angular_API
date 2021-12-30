import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable(
//   {
//   providedIn: 'root'
// }
)

export class EventoService {
  baseURL = "https://localhost:5001/api/evento"

  constructor(private http: HttpClient) { }

  public ObterEventos(): Observable<Evento[]>{
    return this.http.get<Evento[]>(this.baseURL)
  }

  public ObterEventosByTema(tema: string): Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`)
  }

  public ObterEventoById(id: number): Observable<Evento>{
    return this.http.get<Evento>(`${this.baseURL}/${id}`)
  }

  public post(evento: Evento): Observable<Evento>{
    return this.http.post<Evento>(this.baseURL,evento)
  }

  public put(evento: Evento): Observable<Evento>{
    return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento)
  }

  public deleteEvento(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`)
  }
}
