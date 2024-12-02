export class  LicenseDetails
{

    licenseType: string|null=''
   
    sourceList:any|null ={}
    targetList:any|null = {}

   
    constructor(sourceList?:any,targetList?:any,init?:Partial<LicenseDetails>) {
       Object.assign(this,init);
        this.sourceList=sourceList;
        this.targetList=targetList;
    }
}