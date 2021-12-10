import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { FormsModule } from '@angular/forms';

import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule  } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { EventosComponent } from './componentes/eventos/eventos.component';
import { PalestrantesComponent } from './componentes/palestrantes/palestrantes.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';
import { PerfilComponent } from './componentes/perfil/perfil.component';
import { ContatosComponent } from './componentes/contatos/contatos.component';

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';
import { AppRoutingModule } from './app-routing.module';
import { EventoService } from './services/Evento.service';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { EventoDetalheComponent } from './componentes/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListagemComponent } from './componentes/eventos/evento-listagem/evento-listagem.component';
import { UserComponent } from './componentes/user/user.component';
import { LoginComponent } from './componentes/user/login/login.component';
import { RegistrarUsuarioComponent } from './componentes/user/registrar-usuario/registrar-usuario.component';


@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavComponent,
    DateTimeFormatPipe,
    TituloComponent,
    ContatosComponent,
    PerfilComponent,
    DashboardComponent,
    EventoDetalheComponent,
    EventoListagemComponent,
    UserComponent,
    LoginComponent,
    RegistrarUsuarioComponent

   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgxSpinnerModule,
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
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [EventoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
