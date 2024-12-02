export class step {
  stepName: string | undefined;
  index: number | undefined;
  step: string | undefined;
  title: string | undefined;
  moveNext: boolean | undefined;
  status: string = 'Pending';
  constructor(init: Partial<step>) {
    Object.assign(this, init);
  }

  static listFactory(init: Partial<step>[]): step[] {
    return init.map((item, ind) => {
      item.index = ind
      item.step = 'step' + (item.index + 1)
      return new step(item)
    });
  }
}
