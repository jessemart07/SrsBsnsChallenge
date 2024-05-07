import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { emailRequest } from '../models/emailReq';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { contactForm } from '../models/contactForm';

@Injectable({
  providedIn: 'root', // Register the service at the root level
})
export class EmailService {
  constructor(private http: HttpClient) { }
  baseUrl: string = environment.baseApiUrl;

  sendEmail(EmailRequest: emailRequest): Observable<any> {
    return this.http.post<contactForm>(`${this.baseUrl}api/Email`, EmailRequest).pipe(
      catchError((error) => {
        console.error('Email sending failed:', error);
        // Handle the error 
        return new error('Email sending failed. Please try again later.');
      })
    );
  }
}
