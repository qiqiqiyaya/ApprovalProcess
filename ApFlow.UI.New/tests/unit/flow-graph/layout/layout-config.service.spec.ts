/**
 * Unit tests for LayoutConfigService
 * Tests configuration updates, preset application, localStorage persistence, and validation
 */

import { TestBed } from '@angular/core/testing';
import { LayoutConfigService } from '../../../../src/app/pages/flow-graph/services/layout/layout-config.service';
import { LayoutConfig, LAYOUT_PRESETS } from '../../../../src/app/pages/flow-graph/models/layout-config';

describe('LayoutConfigService', () => {
  let service: LayoutConfigService;
  let localStorageSpy: jasmine.SpyObj<Storage>;

  const defaultConfig: LayoutConfig = {
    verticalSpacing: 50,
    horizontalSpacing: 100,
    animate: true,
    animationDuration: 300,
  };

  beforeEach(() => {
    // Setup localStorage spy
    const localStorageMock = {
      getItem: jasmine.createSpy('getItem'),
      setItem: jasmine.createSpy('setItem'),
      removeItem: jasmine.createSpy('removeItem'),
      clear: jasmine.createSpy('clear'),
      length: 0,
      key: jasmine.createSpy('key'),
    };
    localStorageSpy = localStorageMock as jasmine.SpyObj<Storage>;

    TestBed.configureTestingModule({
      providers: [
        LayoutConfigService,
        { provide: 'LOCALSTORAGE', useValue: localStorageSpy },
      ],
    });

    service = TestBed.inject(LayoutConfigService);
  });

  afterEach(() => {
    localStorageSpy.clear.calls.reset();
  });

  describe('T056: Configuration updates', () => {
    it('should emit new configuration when updateConfig is called', (done) => {
      const newConfig: LayoutConfig = {
        verticalSpacing: 80,
        horizontalSpacing: 150,
        animate: false,
        animationDuration: 500,
      };

      service.config$.subscribe((config) => {
        expect(config).toEqual(newConfig);
        done();
      });

      service.updateConfig(newConfig);
    });

    it('should merge partial configuration with existing config', (done) => {
      const partialConfig: Partial<LayoutConfig> = {
        verticalSpacing: 100,
      };

      service.config$.subscribe((config) => {
        expect(config.verticalSpacing).toBe(100);
        expect(config.horizontalSpacing).toBe(100); // Should keep original value
        expect(config.animate).toBe(true); // Should keep original value
        done();
      });

      service.updateConfig(partialConfig as LayoutConfig);
    });

    it('should emit configuration multiple times when updated repeatedly', () => {
      const emissions: LayoutConfig[] = [];

      service.config$.subscribe((config) => {
        emissions.push(config);
      });

      service.updateConfig({ verticalSpacing: 30, horizontalSpacing: 80, animate: true });
      service.updateConfig({ verticalSpacing: 40, horizontalSpacing: 90, animate: true });
      service.updateConfig({ verticalSpacing: 50, horizontalSpacing: 100, animate: true });

      expect(emissions.length).toBe(4); // Initial + 3 updates
      expect(emissions[3].verticalSpacing).toBe(50);
    });

    it('should have initial default configuration', (done) => {
      service.config$.subscribe((config) => {
        expect(config).toEqual(defaultConfig);
        done();
      });
    });
  });

  describe('T057: Preset application', () => {
    it('should return all available presets', () => {
      const presets = service.getPresets();

      expect(presets.length).toBe(3);
      expect(presets.map(p => p.name)).toEqual(['Compact', 'Standard', 'Spacious']);
    });

    it('should apply Compact preset', (done) => {
      service.config$.subscribe((config) => {
        expect(config.verticalSpacing).toBe(30);
        expect(config.horizontalSpacing).toBe(80);
        expect(config.animationDuration).toBe(200);
        done();
      });

      service.applyPreset('Compact');
    });

    it('should apply Standard preset', (done) => {
      service.config$.subscribe((config) => {
        expect(config.verticalSpacing).toBe(50);
        expect(config.horizontalSpacing).toBe(100);
        expect(config.animationDuration).toBe(300);
        done();
      });

      service.applyPreset('Standard');
    });

    it('should apply Spacious preset', (done) => {
      service.config$.subscribe((config) => {
        expect(config.verticalSpacing).toBe(80);
        expect(config.horizontalSpacing).toBe(150);
        expect(config.animationDuration).toBe(400);
        done();
      });

      service.applyPreset('Spacious');
    });

    it('should not emit when applying same preset as current', (done) => {
      let emissionCount = 0;

      service.config$.subscribe(() => {
        emissionCount++;
        if (emissionCount === 1) {
          // First emission (initial config)
          service.applyPreset('Standard'); // Same as default
        } else if (emissionCount === 2) {
          // Should not reach here
          fail('Should not emit duplicate configuration');
        }
      });

      // Wait to verify no second emission
      setTimeout(done, 100);
    });

    it('should throw error when applying non-existent preset', () => {
      expect(() => {
        service.applyPreset('NonExistentPreset');
      }).toThrowError('Preset "NonExistentPreset" not found');
    });
  });

  describe('T058: localStorage persistence', () => {
    it('should save configuration to localStorage when updated', () => {
      const newConfig: LayoutConfig = {
        verticalSpacing: 70,
        horizontalSpacing: 120,
        animate: false,
      };

      service.updateConfig(newConfig);

      expect(localStorageSpy.setItem).toHaveBeenCalledWith(
        'layout-config',
        JSON.stringify(newConfig)
      );
    });

    it('should load configuration from localStorage on initialization', (done) => {
      const savedConfig: LayoutConfig = {
        verticalSpacing: 60,
        horizontalSpacing: 110,
        animate: true,
        animationDuration: 250,
      };

      localStorageSpy.getItem.and.returnValue(JSON.stringify(savedConfig));

      // Recreate service to test initialization
      service = TestBed.inject(LayoutConfigService);

      service.config$.subscribe((config) => {
        expect(config).toEqual(savedConfig);
        done();
      });
    });

    it('should use default config when localStorage is empty', (done) => {
      localStorageSpy.getItem.and.returnValue(null);

      service.config$.subscribe((config) => {
        expect(config).toEqual(defaultConfig);
        done();
      });
    });

    it('should handle corrupted localStorage data gracefully', (done) => {
      localStorageSpy.getItem.and.returnValue('{invalid json');

      service.config$.subscribe((config) => {
        expect(config).toEqual(defaultConfig);
        done();
      });
    });

    it('should clear localStorage when resetToDefaults is called', () => {
      localStorageSpy.getItem.and.returnValue(JSON.stringify(defaultConfig));
      service.resetToDefaults();

      expect(localStorageSpy.removeItem).toHaveBeenCalledWith('layout-config');
    });
  });

  describe('T059: Configuration validation', () => {
    it('should reject verticalSpacing below minimum (20)', () => {
      const invalidConfig: LayoutConfig = {
        ...defaultConfig,
        verticalSpacing: 10,
      };

      expect(() => {
        service.updateConfig(invalidConfig);
      }).toThrowError('verticalSpacing must be between 20 and 200');
    });

    it('should reject verticalSpacing above maximum (200)', () => {
      const invalidConfig: LayoutConfig = {
        ...defaultConfig,
        verticalSpacing: 250,
      };

      expect(() => {
        service.updateConfig(invalidConfig);
      }).toThrowError('verticalSpacing must be between 20 and 200');
    });

    it('should accept valid verticalSpacing values (20-200)', (done) => {
      const validValues = [20, 50, 100, 150, 200];
      let completed = 0;

      service.config$.subscribe((config) => {
        if (completed === validValues.length) {
          done();
        }
      });

      validValues.forEach((value) => {
        expect(() => {
          service.updateConfig({ ...defaultConfig, verticalSpacing: value });
        }).not.toThrow();
        completed++;
      });

      if (completed === validValues.length) {
        done();
      }
    });

    it('should reject horizontalSpacing below minimum (50)', () => {
      const invalidConfig: LayoutConfig = {
        ...defaultConfig,
        horizontalSpacing: 30,
      };

      expect(() => {
        service.updateConfig(invalidConfig);
      }).toThrowError('horizontalSpacing must be between 50 and 500');
    });

    it('should reject horizontalSpacing above maximum (500)', () => {
      const invalidConfig: LayoutConfig = {
        ...defaultConfig,
        horizontalSpacing: 600,
      };

      expect(() => {
        service.updateConfig(invalidConfig);
      }).toThrowError('horizontalSpacing must be between 50 and 500');
    });

    it('should accept valid horizontalSpacing values (50-500)', () => {
      const validValues = [50, 100, 250, 400, 500];

      validValues.forEach((value) => {
        expect(() => {
          service.updateConfig({ ...defaultConfig, horizontalSpacing: value });
        }).not.toThrow();
      });
    });

    it('should reject negative animationDuration', () => {
      const invalidConfig: LayoutConfig = {
        ...defaultConfig,
        animationDuration: -100,
      };

      expect(() => {
        service.updateConfig(invalidConfig);
      }).toThrowError('animationDuration must be non-negative');
    });

    it('should accept zero animationDuration', () => {
      const validConfig: LayoutConfig = {
        ...defaultConfig,
        animationDuration: 0,
      };

      expect(() => {
        service.updateConfig(validConfig);
      }).not.toThrow();
    });

    it('should validate preset configurations', () => {
      LAYOUT_PRESETS.forEach((preset) => {
        expect(() => {
          service.updateConfig(preset.config);
        }).not.toThrow();
      });
    });

    it('should throw multiple validation errors if multiple fields are invalid', () => {
      const invalidConfig: LayoutConfig = {
        verticalSpacing: 10,
        horizontalSpacing: 600,
        animationDuration: -100,
      };

      // Should throw on first validation error
      expect(() => {
        service.updateConfig(invalidConfig);
      }).toThrow();
    });
  });

  describe('getConfigSnapshot', () => {
    it('should return a copy of current configuration', () => {
      const snapshot1 = service.getConfigSnapshot();
      const snapshot2 = service.getConfigSnapshot();

      expect(snapshot1).toEqual(snapshot2);
      expect(snapshot1).not.toBe(snapshot2); // Different object references
    });

    it('should reflect updated configuration', (done) => {
      const newConfig: LayoutConfig = {
        verticalSpacing: 80,
        horizontalSpacing: 150,
        animate: true,
      };

      service.config$.subscribe(() => {
        const snapshot = service.getConfigSnapshot();
        expect(snapshot).toEqual(newConfig);
        done();
      });

      service.updateConfig(newConfig);
    });
  });

  describe('Edge cases', () => {
    it('should handle undefined animate property', () => {
      const configWithoutAnimate: LayoutConfig = {
        verticalSpacing: 50,
        horizontalSpacing: 100,
      };

      expect(() => {
        service.updateConfig(configWithoutAnimate);
      }).not.toThrow();
    });

    it('should handle undefined animationDuration property', () => {
      const configWithoutDuration: LayoutConfig = {
        verticalSpacing: 50,
        horizontalSpacing: 100,
        animate: true,
      };

      expect(() => {
        service.updateConfig(configWithoutDuration);
      }).not.toThrow();
    });

    it('should handle minNodeWidth and minNodeHeight properties', () => {
      const configWithOptionalFields: LayoutConfig = {
        verticalSpacing: 50,
        horizontalSpacing: 100,
        minNodeWidth: 100,
        minNodeHeight: 50,
      };

      expect(() => {
        service.updateConfig(configWithOptionalFields);
      }).not.toThrow();
    });
  });
});
