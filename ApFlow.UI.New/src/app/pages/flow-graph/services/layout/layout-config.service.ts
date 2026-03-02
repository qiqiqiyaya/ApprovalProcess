import { Injectable, Inject, Optional } from '@angular/core';
import { BehaviorSubject, Observable, distinctUntilChanged } from 'rxjs';
import { LayoutConfig, LayoutPreset, LAYOUT_PRESETS, StrictLayoutConfig } from '../../models/layout-config';

/**
 * Service for managing flow graph layout configuration
 * Provides reactive configuration updates, preset management, and localStorage persistence
 */
@Injectable({ providedIn: 'root' })
export class LayoutConfigService {
  /**
   * Storage key for localStorage persistence
   */
  private readonly STORAGE_KEY = 'layout-config';

  /**
   * Default layout configuration
   * Matches the "Standard" preset
   */
  private readonly defaultConfig: LayoutConfig = {
    verticalSpacing: 50,
    horizontalSpacing: 100,
    animate: true,
    animationDuration: 300,
  };

  /**
   * BehaviorSubject for layout configuration state
   * Emits current configuration and allows reactive updates
   */
  private configSubject: BehaviorSubject<LayoutConfig>;

  /**
   * Observable of layout configuration
   * Uses distinctUntilChanged to avoid duplicate emissions
   */
  readonly config$: Observable<LayoutConfig>;

  constructor(@Optional() @Inject('LOCALSTORAGE') private localStorage: Storage) {
    // Load saved configuration from localStorage or use default
    const savedConfig = this.loadFromStorage();
    this.configSubject = new BehaviorSubject<LayoutConfig>(savedConfig);

    // Create observable with deduplication
    this.config$ = this.configSubject.asObservable().pipe(
      distinctUntilChanged((prev, curr) => {
        return JSON.stringify(prev) === JSON.stringify(curr);
      })
    );
  }

  /**
   * T060: Creates LayoutConfigService class
   *
   * Service provides:
   * - Reactive configuration state management
   * - Configuration validation
   * - Preset management
   * - localStorage persistence
   */

  /**
   * T061: Implements BehaviorSubject for configuration state
   * The configSubject holds the current configuration and emits updates to subscribers
   */

  /**
   * T062: Updates layout configuration with validation
   * @param config New layout configuration (partial or complete)
   * @throws Error if configuration is invalid
   */
  updateConfig(config: LayoutConfig | Partial<LayoutConfig>): void {
    // Merge with current config if partial
    const mergedConfig: LayoutConfig = {
      ...this.configSubject.value,
      ...config,
    };

    // Validate configuration
    this.validate(mergedConfig);

    // Update state and persist to storage
    this.configSubject.next(mergedConfig);
    this.saveToStorage(mergedConfig);
  }

  /**
   * T063: Resets configuration to defaults
   * Clears localStorage and restores default values
   */
  resetToDefaults(): void {
    this.configSubject.next(this.defaultConfig);
    this.removeFromStorage();
  }

  /**
   * T064: Saves configuration to localStorage
   * @param config Configuration to save
   */
  private saveToStorage(config: LayoutConfig): void {
    if (this.localStorage) {
      try {
        this.localStorage.setItem(this.STORAGE_KEY, JSON.stringify(config));
      } catch (error) {
        console.warn('[LayoutConfigService] Failed to save config to localStorage:', error);
      }
    }
  }

  /**
   * T064: Loads configuration from localStorage
   * @returns Saved configuration or default if not found
   */
  private loadFromStorage(): LayoutConfig {
    if (!this.localStorage) {
      return this.defaultConfig;
    }

    try {
      const saved = this.localStorage.getItem(this.STORAGE_KEY);
      if (saved) {
        const parsed = JSON.parse(saved) as LayoutConfig;
        this.validate(parsed);
        return parsed;
      }
    } catch (error) {
      console.warn('[LayoutConfigService] Failed to load config from localStorage:', error);
    }

    return this.defaultConfig;
  }

  /**
   * T064: Removes configuration from localStorage
   */
  private removeFromStorage(): void {
    if (this.localStorage) {
      try {
        this.localStorage.removeItem(this.STORAGE_KEY);
      } catch (error) {
        console.warn('[LayoutConfigService] Failed to remove config from localStorage:', error);
      }
    }
  }

  /**
   * T065: Gets available layout presets
   * @returns Array of preset configurations with descriptions
   */
  getPresets(): LayoutPreset[] {
    return LAYOUT_PRESETS;
  }

  /**
   * T065: Applies a preset configuration
   * @param presetName Name of the preset to apply
   * @throws Error if preset not found
   */
  applyPreset(presetName: string): void {
    const preset = LAYOUT_PRESETS.find((p) => p.name === presetName);

    if (!preset) {
      throw new Error(`Preset "${presetName}" not found`);
    }

    // Check if current config already matches preset (avoid unnecessary update)
    const currentConfig = this.configSubject.value;
    const presetConfig = preset.config;

    if (JSON.stringify(currentConfig) === JSON.stringify(presetConfig)) {
      return; // No change needed
    }

    this.updateConfig(preset.config);
  }

  /**
   * T059: Validates configuration values
   * @param config Configuration to validate
   * @throws Error with descriptive message if invalid
   */
  private validate(config: LayoutConfig): void {
    try {
      // Use StrictLayoutConfig for validation
      new StrictLayoutConfig(config);
    } catch (error) {
      if (error instanceof Error) {
        throw new Error(`Invalid layout configuration: ${error.message}`);
      }
      throw error;
    }
  }

  /**
   * Gets current configuration snapshot
   * @returns Copy of current configuration
   */
  getConfigSnapshot(): LayoutConfig {
    return { ...this.configSubject.value };
  }

  /**
   * Gets current configuration value synchronously
   * @returns Current configuration
   */
  getCurrentConfig(): LayoutConfig {
    return this.configSubject.value;
  }
}
