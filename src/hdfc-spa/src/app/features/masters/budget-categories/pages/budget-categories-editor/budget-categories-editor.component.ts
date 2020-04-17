import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UiService } from 'core/services';
import { Status } from 'shared/enums/status.enum';

import { BudgetCategories } from '../../shared/budget-categories';
import { BudgetCategoriesService } from '../../shared/budget-categories.service';

@Component({
  selector: 'app-budget-categories-editor',
  templateUrl: './budget-categories-editor.component.html',
  styles: []
})
export class BudgetCategoriesEditorComponent implements OnInit {
  budgetCategoryForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private uiService: UiService,
    private budgetCategoryService: BudgetCategoriesService,
  ) { }

  ngOnInit(): void {
    const budgetCategoryUid = this.activatedRoute.snapshot.paramMap.get('uid');
    if (budgetCategoryUid) {
      this.budgetCategoryService.getBudgetCategory(budgetCategoryUid).subscribe(res => {
        this.builBudgetCategoriesForm(res);
      });
    } else {
      this.builBudgetCategoriesForm(new BudgetCategories());
    }
  }
  builBudgetCategoriesForm(budgetCategory: BudgetCategories) {
    this.budgetCategoryForm = this.formBuilder.group({
      uid: [budgetCategory.uid],
      name: [budgetCategory.name, Validators.required],
      code: [budgetCategory.code, Validators.required],
      status: [budgetCategory.status],
    });
  }
  get f() {
    return this.budgetCategoryForm.controls;
  }
  onSubmit(any) {
    console.log('values', any);
    if (this.budgetCategoryForm.valid) {
      if (this.f.uid.value) {
        this.uiService.showConfirmationBox({
          message: 'Are You Sure About Updating Budget Category',
          onYes: () => this.updateBudgetCategory()
        });
      } else {
        this.uiService.showConfirmationBox({
          message: 'Are You Sure About Creating Budget Category',
          onYes: () => this.createBudgetCategory()
        });
      }
    }
  }

  createBudgetCategory(): void {
    const budgetCategories = this.extractBudgetCategories();
    this.budgetCategoryService.createBudgetCategories(budgetCategories).subscribe(res => {
      this.uiService.showSuccess(`BudgetCategories: ${res.name} is created successfully.`);
      this.router.navigate([`/masters/budget-categories`]);
    });
  }

  updateBudgetCategory(): void {
    const budgetCategories = this.extractBudgetCategories();
    this.budgetCategoryService.updateBudgetCategories(budgetCategories).subscribe(res => {
      this.uiService.showSuccess(`BudgetCategories: ${res.name} is updated successfully.`);
      this.router.navigate([`/masters/budget-categories`]);
    });
  }
  extractBudgetCategories(): BudgetCategories {
    const budgetCategories: BudgetCategories = {
      uid: this.budgetCategoryForm.value.uid,
      name: this.budgetCategoryForm.value.name,
      code: this.budgetCategoryForm.value.code,
      status:
        this.budgetCategoryForm.value.status === Status.Active ? Status.Active : Status.InActive,
    };
    return budgetCategories;
  }

}
