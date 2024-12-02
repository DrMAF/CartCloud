export class licenseInformation {
  dataBase: string | null = "";
  userName: string | null = "";
  password: string | null = "";
  licenseMaxRecords: string | null = "";
  licenseMaxMedia: string | null = "";
  totalSites: string | null = "";
  multisiteEmail: string | null = "";
  concurrentUsers: string | null = "";
  stockShopUsers: string | null = "";
  assetShopUsers: string | null = "";
  mobileDevices: string | null = "";
  stockShopMobileDevices: string | null = "";

  constructor(init?: Partial<licenseInformation>) {
    Object.assign(this, init);
  }
}
