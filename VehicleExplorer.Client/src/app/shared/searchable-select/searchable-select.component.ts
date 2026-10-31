import {
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

  readonly items = input.required<T[]>();

  readonly displayText = input.required<string>();
  readonly identityValue = input.required<string>();

  readonly placeholder = input('Select an option');
  readonly searchable = input(true);
  readonly loading = input(false);
  readonly icon = input('search');

  readonly searchText = signal('');
  readonly isOpen = signal(false);

  getDisplayText(item: T): string {
    if (typeof item === 'string' || typeof item === 'number') {
      return String(item);
    }

    return String((item as any)[this.displayText()] ?? '');
  }

  readonly filteredItems = computed(() => {
    const items = this.items();

    if (!this.searchable()) {
      return items;
    }

    const search = this.searchText().trim().toLowerCase();

    if (!search) {
      return items;
    }

    return items.filter(item =>
      this.getDisplayText(item).toLowerCase().includes(search)
    );
  });

  isSelected(item: T): boolean {
    const selected = this.value();

    if (selected === null) {
      return false;
    }

    if (typeof item === 'string' || typeof item === 'number') {
      return item === selected;
    }

    return (item as any)[this.identityValue()] == (selected as any)[this.identityValue()];
  }

  onSearchInput(event: Event) {
    const text = (event.target as HTMLInputElement).value;
    this.searchText.set(text);
    this.value.set(null);
    this.isOpen.set(true);
  }

  selectItem(item: T) {
    this.value.set(item);
    this.searchText.set(this.getDisplayText(item));
    this.isOpen.set(false);
  }

  openDropdown() {
    this.isOpen.set(true);
  }

  closeDropdown() {
    this.isOpen.set(false);
  }

  toggleDropdown() {
    this.isOpen.update(open => !open);
  }
}
