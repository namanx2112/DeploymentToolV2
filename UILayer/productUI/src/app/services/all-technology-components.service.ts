import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreInstallation, StoreInteriorMenus, StoreNetworkings, StorePOS, StorePaymentSystem, StoreSonicRadio, StoreStackholders } from '../interfaces/sonic';
import { Dictionary } from '../interfaces/commons';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class AllTechnologyComponentsService {

  constructor(private http: HttpClient, private authService: AuthService) {
  }

  GetStoreContact(request: Dictionary<string>){
    return this.http.post<StoreContact[]>(CommonService.ConfigUrl + "StoreContacts/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateStoreContact(request: StoreContact){
    return this.http.post<StoreContact>(CommonService.ConfigUrl + "StoreContacts/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateStoreContact(request: StoreContact){
    return this.http.post<StoreContact>(CommonService.ConfigUrl + "StoreContacts/update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetStoreConfig(request: Dictionary<string>){
    return this.http.post<StoreConfiguration[]>(CommonService.ConfigUrl + "ProjectConfigs/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateStoreConfig(request: StoreConfiguration){
    return this.http.post<StoreConfiguration>(CommonService.ConfigUrl + "ProjectConfigs/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateStoreConfig(request: StoreConfiguration){
    return this.http.post<StoreConfiguration>(CommonService.ConfigUrl + "ProjectConfigs/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetStackholders(request: Dictionary<string>){
    return this.http.post<StoreStackholders[]>(CommonService.ConfigUrl + "ProjectStakeHolders/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateStackholders(request: StoreStackholders){
    return this.http.post<StoreStackholders>(CommonService.ConfigUrl + "ProjectStakeHolders/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateStackholders(request: StoreStackholders){
    return this.http.post<StoreStackholders>(CommonService.ConfigUrl + "ProjectStakeHolders/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetNetworking(request: Dictionary<string>){
    return this.http.post<StoreNetworkings[]>(CommonService.ConfigUrl + "ProjectNetworkings/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateNetworking(request: StoreNetworkings){
    return this.http.post<StoreNetworkings>(CommonService.ConfigUrl + "ProjectNetworkings/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateNetworking(request: StoreNetworkings){
    return this.http.post<StoreNetworkings>(CommonService.ConfigUrl + "ProjectNetworkings/update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetPOS(request: Dictionary<string>){
    return this.http.post<StorePOS[]>(CommonService.ConfigUrl + "ProjectPOS/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreatePOS(request: StorePOS){
    return this.http.post<StorePOS>(CommonService.ConfigUrl + "ProjectPOS/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdatePOS(request: StorePOS){
    return this.http.post<StorePOS>(CommonService.ConfigUrl + "ProjectPOS/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetAudio(request: Dictionary<string>){
    return this.http.post<StoreAudio[]>(CommonService.ConfigUrl + "ProjectAudios/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateAudio(request: StoreAudio){
    return this.http.post<StoreAudio>(CommonService.ConfigUrl + "ProjectAudios/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateAudio(request: StoreAudio){
    return this.http.post<StoreAudio>(CommonService.ConfigUrl + "ProjectAudios/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetExteriorMenus(request: Dictionary<string>){
    return this.http.post<StoreExteriorMenus[]>(CommonService.ConfigUrl + "ProjectExteriorMenus/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateExteriorMenus(request: StoreExteriorMenus){
    return this.http.post<StoreExteriorMenus>(CommonService.ConfigUrl + "ProjectExteriorMenus/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateExteriorMenus(request: StoreExteriorMenus){
    return this.http.post<StoreExteriorMenus>(CommonService.ConfigUrl + "ProjectExteriorMenus/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetPaymentSystem(request: Dictionary<string>){
    return this.http.post<StorePaymentSystem[]>(CommonService.ConfigUrl + "ProjectPaymentSystems/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreatePaymentSystem(request: StorePaymentSystem){
    return this.http.post<StorePaymentSystem>(CommonService.ConfigUrl + "ProjectPaymentSystems/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdatePaymentSystem(request: StorePaymentSystem){
    return this.http.post<StorePaymentSystem>(CommonService.ConfigUrl + "ProjectPaymentSystems/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetInteriorMenus(request: Dictionary<string>){
    return this.http.post<StoreInteriorMenus[]>(CommonService.ConfigUrl + "ProjectInteriorMenus/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateInteriorMenus(request: StoreInteriorMenus){
    return this.http.post<StoreInteriorMenus>(CommonService.ConfigUrl + "ProjectInteriorMenus/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateInteriorMenus(request: StoreInteriorMenus){
    return this.http.post<StoreInteriorMenus>(CommonService.ConfigUrl + "ProjectInteriorMenus/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetSonicRadio(request: Dictionary<string>){
    return this.http.post<StoreSonicRadio[]>(CommonService.ConfigUrl + "ProjectSonicRadios/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateSonicRadio(request: StoreSonicRadio){
    return this.http.post<StoreSonicRadio>(CommonService.ConfigUrl + "ProjectSonicRadios/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateSonicRadio(request: StoreSonicRadio){
    return this.http.post<StoreSonicRadio>(CommonService.ConfigUrl + "ProjectSonicRadios/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetInstallation(request: Dictionary<string>){
    return this.http.post<StoreInstallation[]>(CommonService.ConfigUrl + "ProjectInstallations/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CreateInstallation(request: StoreInstallation){
    return this.http.post<StoreInstallation>(CommonService.ConfigUrl + "ProjectInstallations/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateInstallation(request: StoreInstallation){
    return this.http.post<StoreInstallation>(CommonService.ConfigUrl + "ProjectInstallations/update", request, { headers: this.authService.getHttpHeaders() });
  }
}
