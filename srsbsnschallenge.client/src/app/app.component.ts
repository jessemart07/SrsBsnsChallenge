import { Component, OnInit, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { contactForm } from './models/contactForm';
import { ContactFormService } from './services/contactForm.services';
import { emailRequest } from './models/emailReq';
import { EmailService } from './services/email.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  contactFormService = inject(ContactFormService);
  emailService = inject(EmailService);
  contactForm = new FormGroup({
    name: new FormControl(''),
    subject: new FormControl(''),
    email: new FormControl(''),
    message: new FormControl(''),
  });
  submissionMessage = "";
  formError = false;
  submitted = false;
  isLoading = false;
 
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.contactForm = this.formBuilder.group({
      name: ['', Validators.required],
      subject: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      message: ['', Validators.required],
    })
  }

  get f(): { [key: string]: AbstractControl } {
    return this.contactForm.controls;
  }


  submitForm() {
    this.isLoading = true;
    this.submitted = true;
    
    if (!this.contactForm.invalid) {
      const ContactForm: contactForm = {
        name: this.contactForm.value.name ?? '',
        subject: this.contactForm.value.subject ?? '',
        email: this.contactForm.value.email ?? '',
        message: this.contactForm.value.message ?? ''
      }

      const UserEmailRequest: emailRequest = {
        name: ContactForm.name,
        subject: "Thank You!",
        to: ContactForm.email,
        body: `<html lang="en">
                  <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Thank You!</title>
                  </head>
                  <body style="font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 0; background-color: #f8f8f8;">
                    <div style="max-width: 600px; margin: auto; padding: 20px;">
                      <h1 style="color: #2a9d8f;">Thank You!</h1>
                      <p>Hey ${ContactForm.name},</p>
                      <p>Thank you for contacting us. We have received your message and will get back to you as soon as possible.</p>
                      <p>Best regards,</p>
                      <p>The Team</p>
                    </div>
                  </body>
                </html>`
      }
      const AdminEmailRequest: emailRequest = {
        name: ContactForm.name,
        subject: "New Form Submission",
        to: "colin@srsbsns.co.za",
        body: `<html lang="en">
                  <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>New Submission</title>
                  </head>
                  <body style="font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 0; background-color: #f8f8f8;">
                    <div style="max-width: 600px; margin: auto; padding: 20px;">
                      <h1 style="color: #2a9d8f;">New Form Submission</h1>
                      <p>Hey Admin,</p>
                      <p>You have received a new form submission. Here's the data:</p>
                      <p>Name: ${ContactForm.name}</p>
                      <p>Subject: ${ContactForm.subject}</p>
                      <p>Email: ${ContactForm.email}</p>
                      <p>Message: ${ContactForm.message}</p>
                    </div>
                  </body>
                </html>`
      }

      this.contactFormService.submitForm(ContactForm).subscribe(
        {
          next: response => {
            console.log("data saved")
            this.emailService.sendEmail(UserEmailRequest).subscribe(
              {
                next: () => {
                  console.log("user email sent");
                  this.emailService.sendEmail(AdminEmailRequest).subscribe(
                    {
                      next: () => {
                        console.log("admin email sent");
                        this.submissionMessage = "Thank you for reaching out! We will get back to you soon";
                        this.formError = false;
                        this.submitted = false;
                        this.isLoading = false;
                        this.contactForm.reset();
                      },
                      error: () => {
                        this.submissionMessage = "Oops! Something went wrong. Please try again.";
                        this.formError = true;
                        this.isLoading = false;
                      }
                    }
                  )
                },
                error: (error) => {
                  this.submissionMessage = "Oops! Something went wrong. Please try again.";
                  this.isLoading = false;
                  this.formError = true;
                }
              }
            )
          },
          error: error => {
            this.submissionMessage = "Oops! Something went wrong. Please try again.";
            this.isLoading = false;
            this.formError = true;
          }
        }) 
    } else {
      this.isLoading = false;
      return;
    }
  }

  title = 'srsbsnschallenge.client';
}
