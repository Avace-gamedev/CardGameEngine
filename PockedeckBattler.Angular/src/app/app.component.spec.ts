import { AppComponent } from './app.component';
import { MockBuilder, MockRender } from 'ng-mocks';
import { AppModule } from './app.module';

describe('App', () => {
  beforeEach(() => MockBuilder(AppComponent, AppModule));

  it('should create', () => {
    const fixture = MockRender(AppComponent);

    expect(fixture.point.componentInstance).toBeTruthy();
  });
});
