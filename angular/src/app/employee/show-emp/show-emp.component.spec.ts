import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowEmpComponent } from './show-emp.component';

describe('ShowEmpComponent', () => {
  let component: ShowEmpComponent;
  let fixture: ComponentFixture<ShowEmpComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShowEmpComponent]
    });
    fixture = TestBed.createComponent(ShowEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
