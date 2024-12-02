export class SupportDetails {
  supportPlan: string | null = "";
  experationDate: string | null = "";
  activate: boolean | null = false;

    constructor(init?:Partial<SupportDetails>) {
        Object.assign(this,init);         
     }
}
