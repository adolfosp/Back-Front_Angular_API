import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { FormsModule } from '@angular/forms';


import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule  } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { NavComponent } from './nav/nav.component';

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';

import { AppRoutingModule } from './app-routing.module';

import { EventoService } from './services/Evento.service';

import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavComponent,
    DateTimeFormatPipe
   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot(
      { timeOut: 3000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
        progressBar: true
      }
    )
  ],
  providers: [EventoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
