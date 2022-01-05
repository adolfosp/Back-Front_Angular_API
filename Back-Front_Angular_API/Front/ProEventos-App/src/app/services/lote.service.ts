import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class LoteService {

  baseURL = "https://localhost:5001/api/lotes"

  constructor(private http: HttpClient) { }

  public obterLotesPorEventoId(eventoId: number): Observable<Lote[]>{
    return this.http
    .get<Lote[]>(`${this.baseURL}/${eventoId}`)
    .pipe(take(1));
    //executa a quantidade de vezes que eu colocar dentro do take e depois da um unsubscribe
  }

  public salvarLote(eventoId: number, lotes: Lote[]): Observable<Lote>{
    return this.http
    .put<Lote>(`${this.baseURL}/${eventoId}`, lotes)
    .pipe(take(1));

  }

  public deleteEvento(eventoid: number, loteId: number): Observable<any>{
    return this.http
    .delete(`${this.baseURL}/${eventoid}/${loteId}`)
    .pipe(take(1));

  }
}
