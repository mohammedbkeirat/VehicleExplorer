import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
  model,
  signal
} from '@angular/core';
import { FormValueControl } from '@angular/forms/signals';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-searchable-select',
  imports: [MatIconModule],
  templateUrl: './searchable-select.component.html',
  styleUrl: './searchable-select.component.scss',
})

export class SearchableSelectComponent<T>
  implements FormValueControl<T | null> {

  readonly value = model<T | null>(null);

  readonly options = input.required<T[]>();
  readonly displayWith = input.required<(option: T) => string>();

  readonly placeholder = input('Select an option');
  readonly searchable = input(true);
  readonly loading = input(false);

  readonly searchText = signal('');
  readonly isOpen = signal(false);

  readonly filteredOptions = computed(() => {
    const options = this.options();

    if (!this.searchable()) {
      return options;
    }

    const search = this.searchText().trim().toLowerCase();

    if (!search) {
      return options;
    }

    return options.filter(option =>
      this.displayWith()(option).toLowerCase().includes(search)
    );
  });

  onSearchInput(event: Event): void {
    const text = (event.target as HTMLInputElement).value;

    this.searchText.set(text);
    this.value.set(null);
    this.isOpen.set(true);
  }

  selectOption(option: T): void {
    this.value.set(option);
    this.searchText.set(this.displayWith()(option));
    this.isOpen.set(false);
  }

  openDropdown(): void {
    this.isOpen.set(true);
  }

  closeDropdown(): void {
    this.isOpen.set(false);
  }

  toggleDropdown(): void {
    this.isOpen.update(open => !open);
  }
}
