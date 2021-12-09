import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoListagemComponent } from './evento-listagem.component';

describe('EventoListagemComponent', () => {
  let component: EventoListagemComponent;
  let fixture: ComponentFixture<EventoListagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventoListagemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoListagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
