import { expect, Page } from '@playwright/test';
import { StepsBase } from './StepsBase';


export class LoginSteps extends StepsBase {
  public constructor(page: Page) {
    super(page);
  }

  public async FillUsername(username: string): Promise<void> {
    var usernameInput = this.page.locator('id=Input_Username');
    await expect(usernameInput).toBeVisible();

    await usernameInput.fill(username);
  }

  public async FillPassword(password: string): Promise<void> {
    var usernameInput = this.page.locator('id=Input_Password');
    await expect(usernameInput).toBeVisible();

    await usernameInput.fill(password);
  }

  public async ClickLoginButton(): Promise<void> {
    var loginButton = this.page.locator('id=Button_Login');
    await expect(loginButton).toBeVisible();

    await loginButton.click();
  }
}
