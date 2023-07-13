import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { LoginComponent } from './components/Auth/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { NewEditObjectComponent } from './components/new-edit-object/new-edit-object.component';

const routes: Routes =  [
  { path: 'login', component: LoginComponent },
  { path: 'NewProd/UILayer/ui', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'newEdit', component: NewEditObjectComponent },
  { path: '**', component: LoginComponent}
]; // sets up routes constant where you define your routes

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
