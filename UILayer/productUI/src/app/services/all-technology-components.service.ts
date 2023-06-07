import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { StoreAudio, StoreConfiguration, StoreContact, StoreExteriorMenus, StoreInstallation, StoreInteriorMenus, StoreNetworkings, StorePOS, StorePaymentSystem, StoreSonicRadio, StoreStackholders } from '../interfaces/sonic';
import { Dictionary } from '../interfaces/commons';

@Injectable({
  providedIn: 'root'
})
export class AllTechnologyComponentsService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
  }

  GetStoreContact(request: Dictionary<string>){
    return this.http.post<StoreContact[]>(this.configUrl + "ProjectStoreContacts/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateStoreContact(request: StoreContact){
    return this.http.post<StoreContact>(this.configUrl + "ProjectStoreContacts/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateStoreContact(request: StoreContact){
    return this.http.post<StoreContact>(this.configUrl + "ProjectStoreContacts/update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetStoreConfig(request: Dictionary<string>){
    return this.http.post<StoreConfiguration[]>(this.configUrl + "ProjectConfigs/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateStoreConfig(request: StoreConfiguration){
    return this.http.post<StoreConfiguration>(this.configUrl + "ProjectConfigs/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateStoreConfig(request: StoreConfiguration){
    return this.http.post<StoreConfiguration>(this.configUrl + "ProjectConfigs/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetStackholders(request: Dictionary<string>){
    return this.http.post<StoreStackholders[]>(this.configUrl + "ProjectStakeHolders/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateStackholders(request: StoreStackholders){
    return this.http.post<StoreStackholders>(this.configUrl + "ProjectStakeHolders/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateStackholders(request: StoreStackholders){
    return this.http.post<StoreStackholders>(this.configUrl + "ProjectStakeHolders/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetNetworking(request: Dictionary<string>){
    return this.http.post<StoreNetworkings[]>(this.configUrl + "ProjectNetworkings/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateNetworking(request: StoreNetworkings){
    return this.http.post<StoreNetworkings>(this.configUrl + "ProjectNetworkings/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateNetworking(request: StoreNetworkings){
    return this.http.post<StoreNetworkings>(this.configUrl + "ProjectNetworkings/update", request, { headers: this.authService.getHttpHeaders() });
  }

  GetPOS(request: Dictionary<string>){
    return this.http.post<StorePOS[]>(this.configUrl + "ProjectPOS/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeatePOS(request: StorePOS){
    return this.http.post<StorePOS>(this.configUrl + "ProjectPOS/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdatePOS(request: StorePOS){
    return this.http.post<StorePOS>(this.configUrl + "ProjectPOS/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetAudio(request: Dictionary<string>){
    return this.http.post<StoreAudio[]>(this.configUrl + "ProjectAudios/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateAudio(request: StoreAudio){
    return this.http.post<StoreAudio>(this.configUrl + "ProjectAudios/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateAudio(request: StoreAudio){
    return this.http.post<StoreAudio>(this.configUrl + "ProjectAudios/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetExteriorMenus(request: Dictionary<string>){
    return this.http.post<StoreExteriorMenus[]>(this.configUrl + "ProjectExteriorMenus/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateExteriorMenus(request: StoreExteriorMenus){
    return this.http.post<StoreExteriorMenus>(this.configUrl + "ProjectExteriorMenus/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateExteriorMenus(request: StoreExteriorMenus){
    return this.http.post<StoreExteriorMenus>(this.configUrl + "ProjectExteriorMenus/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetPaymentSystem(request: Dictionary<string>){
    return this.http.post<StorePaymentSystem[]>(this.configUrl + "ProjectPaymentSystems/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeatePaymentSystem(request: StorePaymentSystem){
    return this.http.post<StorePaymentSystem>(this.configUrl + "ProjectPaymentSystems/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdatePaymentSystem(request: StorePaymentSystem){
    return this.http.post<StorePaymentSystem>(this.configUrl + "ProjectPaymentSystems/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetInteriorMenus(request: Dictionary<string>){
    return this.http.post<StoreInteriorMenus[]>(this.configUrl + "ProjectInteriorMenus/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateInteriorMenus(request: StoreInteriorMenus){
    return this.http.post<StoreInteriorMenus>(this.configUrl + "ProjectInteriorMenus/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateInteriorMenus(request: StoreInteriorMenus){
    return this.http.post<StoreInteriorMenus>(this.configUrl + "ProjectInteriorMenus/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetSonicRadio(request: Dictionary<string>){
    return this.http.post<StoreSonicRadio[]>(this.configUrl + "ProjectSonicRadios/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateSonicRadio(request: StoreSonicRadio){
    return this.http.post<StoreSonicRadio>(this.configUrl + "ProjectSonicRadios/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateSonicRadio(request: StoreSonicRadio){
    return this.http.post<StoreSonicRadio>(this.configUrl + "ProjectSonicRadios/update", request, { headers: this.authService.getHttpHeaders() });
  }


  GetInstallation(request: Dictionary<string>){
    return this.http.post<StoreInstallation[]>(this.configUrl + "ProjectInstallations/Get", request, { headers: this.authService.getHttpHeaders() });
  }
  CeateInstallation(request: StoreInstallation){
    return this.http.post<StoreInstallation>(this.configUrl + "ProjectInstallations/Create", request, { headers: this.authService.getHttpHeaders() });
  }
  UpdateInstallation(request: StoreInstallation){
    return this.http.post<StoreInstallation>(this.configUrl + "ProjectInstallations/update", request, { headers: this.authService.getHttpHeaders() });
  }
}
