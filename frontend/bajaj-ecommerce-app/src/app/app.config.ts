import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import routes from './app.routes'
import { provideRouter } from '@angular/router';
import { tokenInterceptor } from './core/interceptors/token-interceptor';
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withFetch(),withInterceptors([tokenInterceptor])),
    provideRouter(routes)
  ]
};
