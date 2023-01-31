import { Page } from '@playwright/test';

export abstract class StepsBase {
  protected readonly page: Page;

  constructor(page: Page) {
    this.page = page;
  }
}
