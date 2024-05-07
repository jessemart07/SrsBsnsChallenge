import { Injectable } from '@angular/core';
import { contactForm } from '../models/contactForm';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { emailRequest } from '../models/emailReq';

@Injectable({
  providedIn: 'root',
})
export class ContactFormService {
  
  constructor(private http: HttpClient) { }
  baseUrl : string = environment.baseApiUrl;

  submitForm(contactForm: contactForm): Observable<any> {
      return this.http.post<contactForm>(`${this.baseUrl}api/ContactForm`, contactForm);
  }
}
