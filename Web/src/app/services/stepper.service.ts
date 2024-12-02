import { AfterViewInit, Injectable } from '@angular/core';
import { step } from '../models/step';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StepperService implements AfterViewInit {
  currentStep=new BehaviorSubject<number>(0);
  steps = new Subject<step[]>;
  streamedSteps = new Subject<Partial<step>>();
  stepSet: Partial<step>[] = [];

  constructor() {
    this.streamedSteps.subscribe(e => {
      this.addAndUpdateStep(e);
      this.steps.next(step.listFactory([...this.stepSet]))
    })

  }

  ngAfterViewInit(): void {

  }
  addAndUpdateStep(step: Partial<step>): void {
    var existingStep = this.stepSet.findIndex(s => s.stepName == step.stepName);
    if (~existingStep) {
       this.stepSet[existingStep] = step;
    } else {
      this.stepSet.push(step)
    }
    // this.stepSet.has()

  }

  reset(){

    this.currentStep.next(0)
    this.stepSet.forEach(s=>s.status='pending')
    this.steps.next(step.listFactory([...this.stepSet]))

  }

  

}
