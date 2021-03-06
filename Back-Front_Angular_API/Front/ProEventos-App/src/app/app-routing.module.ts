import { RegistrarUsuarioComponent } from './componentes/user/registrar-usuario/registrar-usuario.component';
import { LoginComponent } from './componentes/user/login/login.component';
import { UserComponent } from './componentes/user/user.component';
import { PerfilComponent } from './componentes/user/perfil/perfil.component';

import { EventoListagemComponent } from './componentes/eventos/evento-listagem/evento-listagem.component';
import { EventoDetalheComponent } from './componentes/eventos/evento-detalhe/evento-detalhe.component';
import { EventosComponent } from './componentes/eventos/eventos.component';

import { PalestrantesComponent } from './componentes/palestrantes/palestrantes.component';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';
import { ContatosComponent } from './componentes/contatos/contatos.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      {path: 'login', component: LoginComponent},
      {path: 'registrar', component: RegistrarUsuarioComponent},

    ]

  },
  {path: 'user/perfil', component: PerfilComponent},
  {path: 'eventos', redirectTo: 'eventos/lista'},
  {
    path: 'eventos', component: EventosComponent,
    children:[
      {path: 'detalhe/:id', component: EventosComponent},
      {path: 'detalhe', component: EventoDetalheComponent},
      {path: 'lista', component: EventoListagemComponent},
    ]
  },
  {path: 'dashboard', component: DashboardComponent},
  {path: 'contatos', component: ContatosComponent},
  {path: 'palestrantes', component: PalestrantesComponent},
  {path: '**', redirectTo: 'dashboard', pathMatch :'full'},
  {path: '', redirectTo:'dashboard', pathMatch :'full'},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
