// auto restore for jasmine and jest <27
// declare const jasmine: any;
import { MockInstance, MockService, ngMocks } from 'ng-mocks'; // eslint-disable-line import/order // eslint-disable-line import/order // eslint-disable-line import/order
// In case, if you use @angular/router and Angular 14+.
// You might want to set a mock of DefaultTitleStrategy as TitleStrategy.
// A14 fix: making DefaultTitleStrategy to be a default mock for TitleStrategy
import { DefaultTitleStrategy, TitleStrategy } from '@angular/router'; // eslint-disable-line import/order
// Usually, *ngIf and other declarations from CommonModule aren't expected to be mocked.
// The code below keeps them.
import { CommonModule } from '@angular/common'; // eslint-disable-line import/order
import { ApplicationModule } from '@angular/core'; // eslint-disable-line import/order
import { BrowserModule } from '@angular/platform-browser'; // eslint-disable-line import/order

// auto spy
ngMocks.autoSpy('jasmine');
ngMocks.defaultMock(TitleStrategy, () => MockService(DefaultTitleStrategy));

ngMocks.globalKeep(ApplicationModule, true);
ngMocks.globalKeep(CommonModule, true);
ngMocks.globalKeep(BrowserModule, true);

jasmine.getEnv().addReporter({
  specDone: MockInstance.restore,
  specStarted: MockInstance.remember,
  suiteDone: MockInstance.restore,
  suiteStarted: MockInstance.remember,
});
