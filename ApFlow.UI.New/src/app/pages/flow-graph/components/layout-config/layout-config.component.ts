import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { LayoutConfigService } from '../../services/layout/layout-config.service';
import { LayoutConfig, LAYOUT_PRESETS, LayoutPreset } from '../../models/layout-config';

/**
 * Component for configuring flow graph layout parameters
 * Provides UI controls for spacing, animation, and preset selection
 *
 * T067: Creates LayoutConfigComponent class
 */
@Component({
  selector: 'app-layout-config',
  templateUrl: './layout-config.component.html',
  styleUrls: ['./layout-config.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
})
export class LayoutConfigComponent implements OnInit, OnDestroy {
  /**
   * Reactive form for layout configuration
   * T069: Implements ReactiveForm with ng-zorro-antd components
   */
  layoutForm: FormGroup;

  /**
   * Available layout presets
   */
  presets: LayoutPreset[] = LAYOUT_PRESETS;

  /**
   * Subscription to configuration changes
   */
  private configSubscription: Subscription;

  /**
   * Currently applied preset name (if any)
   */
  private currentPreset: string | null = 'Standard';

  constructor(
    private fb: FormBuilder,
    private layoutConfigService: LayoutConfigService
  ) {
    // Initialize form with validation
    this.layoutForm = this.fb.group({
      verticalSpacing: [50, [Validators.min(20), Validators.max(200)]],
      horizontalSpacing: [100, [Validators.min(50), Validators.max(500)]],
      animate: [true],
      animationDuration: [300, [Validators.min(0)]],
      preset: ['Standard'],
    });

    this.configSubscription = Subscription.EMPTY;
  }

  /**
   * Initialize component and subscribe to configuration changes
   */
  ngOnInit(): void {
    // Subscribe to configuration changes to update form
    this.configSubscription = this.layoutConfigService.config$.subscribe((config) => {
      this.layoutForm.patchValue({
        verticalSpacing: config.verticalSpacing,
        horizontalSpacing: config.horizontalSpacing,
        animate: config.animate ?? true,
        animationDuration: config.animationDuration ?? 300,
      }, { emitEvent: false });
    });
  }

  /**
   * Clean up subscriptions on component destroy
   */
  ngOnDestroy(): void {
    this.configSubscription.unsubscribe();
  }

  /**
   * T074: Handle form submission
   * Updates layout configuration with form values
   */
  onSubmit(): void {
    if (this.layoutForm.valid) {
      const config: LayoutConfig = {
        verticalSpacing: this.layoutForm.value.verticalSpacing,
        horizontalSpacing: this.layoutForm.value.horizontalSpacing,
        animate: this.layoutForm.value.animate,
        animationDuration: this.layoutForm.value.animationDuration,
      };

      this.layoutConfigService.updateConfig(config);
      this.currentPreset = null; // Clear preset indicator after manual changes
    }
  }

  /**
   * T074: Handle form reset
   * Resets form to current configuration values
   */
  onReset(): void {
    const config = this.layoutConfigService.getConfigSnapshot();
    this.layoutForm.patchValue({
      verticalSpacing: config.verticalSpacing,
      horizontalSpacing: config.horizontalSpacing,
      animate: config.animate ?? true,
      animationDuration: config.animationDuration ?? 300,
    });
  }

  /**
   * T074: Apply a layout preset
   * @param presetName Name of the preset to apply
   */
  applyPreset(presetName: string): void {
    try {
      this.layoutConfigService.applyPreset(presetName);
      this.currentPreset = presetName;
    } catch (error) {
      console.error(`Failed to apply preset "${presetName}":`, error);
    }
  }

  /**
   * Reset to default configuration
   */
  resetToDefaults(): void {
    this.layoutConfigService.resetToDefaults();
    this.currentPreset = 'Standard';
  }

  /**
   * Check if a preset is currently applied
   * @param presetName Name of the preset to check
   * @returns True if the preset is currently active
   */
  isPresetActive(presetName: string): boolean {
    return this.currentPreset === presetName;
  }

  /**
   * Get validation error for a form control
   * @param controlName Name of the form control
   * @returns Error message or null
   */
  getErrorMessage(controlName: string): string | null {
    const control = this.layoutForm.get(controlName);

    if (!control || !control.invalid || !control.touched) {
      return null;
    }

    if (control.hasError('min')) {
      const minValue = control.errors!['min'].min;
      return `Value must be at least ${minValue}`;
    }

    if (control.hasError('max')) {
      const maxValue = control.errors!['max'].max;
      return `Value must be at most ${maxValue}`;
    }

    if (control.hasError('required')) {
      return 'This field is required';
    }

    return 'Invalid value';
  }

  /**
   * T070: Get vertical spacing constraints
   */
  get verticalSpacingMin(): number {
    return 20;
  }

  get verticalSpacingMax(): number {
    return 200;
  }

  /**
   * T071: Get horizontal spacing constraints
   */
  get horizontalSpacingMin(): number {
    return 50;
  }

  get horizontalSpacingMax(): number {
    return 500;
  }

  /**
   * Gets the currently selected preset name
   */
  get presetName(): string | null {
    return this.currentPreset;
  }

  /**
   * Gets description for a preset
   * @param presetName Name of the preset
   * @returns Preset description or empty string
   */
  getPresetDescription(presetName: string): string {
    const preset = this.presets.find(p => p.name === presetName);
    return preset?.description ?? '';
  }

  /**
   * Formatter for spacing values (adds "px" suffix)
   */
  formatterSpacing(value: number): string {
    return `${value} px`;
  }

  /**
   * Parser for spacing values (removes "px" suffix)
   */
  parserSpacing(value: string): number {
    return parseInt(value.replace(' px', ''), 10);
  }

  /**
   * Formatter for duration values (adds "ms" suffix)
   */
  formatterDuration(value: number): string {
    return `${value} ms`;
  }

  /**
   * Parser for duration values (removes "ms" suffix)
   */
  parserDuration(value: string): number {
    return parseInt(value.replace(' ms', ''), 10);
  }
}
