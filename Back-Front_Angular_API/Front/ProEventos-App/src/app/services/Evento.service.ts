import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';
import { take } from 'rxjs/operators';
import { environment } from '@environments/environment';

@Injectable(
//   {
//   providedIn: 'root'
// }
)

export class EventoService {
  baseURL = environment.apiUrl + '/api/evento';

  constructor(private http: HttpClient) { }

  public ObterEventos(): Observable<Evento[]>{
    return this.http
    .get<Evento[]>(this.baseURL)
    .pipe(take(1));
    //executa a quantidade de vezes que eu colocar dentro do take e depois da um unsubscribe
  }

  public ObterEventosByTema(tema: string): Observable<Evento[]>{
    return this.http
    .get<Evento[]>(`${this.baseURL}/${tema}/tema`)
    .pipe(take(1));

  }

  public ObterEventoById(id: number): Observable<Evento>{
    return this.http
    .get<Evento>(`${this.baseURL}/${id}`)
    .pipe(take(1));

  }

  public post(evento: Evento): Observable<Evento>{
    return this.http
    .post<Evento>(this.baseURL,evento)
    .pipe(take(1));

  }

  public put(evento: Evento): Observable<Evento>{
    return this.http
    .put<Evento>(`${this.baseURL}/${evento.id}`, evento)
    .pipe(take(1));

  }

  public deleteEvento(id: number): Observable<any>{
    return this.http
    .delete(`${this.baseURL}/${id}`)
    .pipe(take(1));
  }

  public postUpload(eventoId: number, file: File): Observable<Evento>{
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);
    
    return this.http
    .post<Evento>(`${this.baseURL}/upload-image/${eventoId}`, formData)
    .pipe(take(1));
  }
}


