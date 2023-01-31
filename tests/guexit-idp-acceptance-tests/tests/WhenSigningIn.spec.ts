import { test, expect } from '@playwright/test';
import { LoginSteps } from '../steps/LoginSteps';

test.describe('When signing in', () => {
  const idpBaseUrl: string = 'https://localhost:7192/';
  const idpLoginPage: string = idpBaseUrl + 'Account/Login?ReturnUrl=%2Fdiagnostics';

  test.beforeEach(async ({ page }) => {
    await page.goto(idpLoginPage);
  });

  test('authentication result is successful for a registered user', async ({ page }) => {
    var loginSteps = new LoginSteps(page);
    await loginSteps.FillUsername('pablitocamela@gmail.com');
    await loginSteps.FillPassword('tV/de38vHH3f2v$');
    await loginSteps.ClickLoginButton();

    var claimsSectionTitle = page.getByText('Claims');
    await expect(claimsSectionTitle).toBeVisible();
  });

  test('authentication result is an error for a not registered user', async ({ page }) => {
    var loginSteps = new LoginSteps(page);
    await loginSteps.FillUsername('nonexistinguser@whatever.net');
    await loginSteps.FillPassword('tV/de38vHH3f2v$');
    await loginSteps.ClickLoginButton();

    var authenticationError = page.getByText('Invalid username or password');
    await expect(authenticationError).toBeVisible();
  });
});
