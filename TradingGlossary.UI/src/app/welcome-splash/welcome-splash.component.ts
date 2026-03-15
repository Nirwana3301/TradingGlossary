import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgxThreeGlobeComponent } from '@omnedia/ngx-three-globe';
import { NgxFlickeringGridComponent } from '@omnedia/ngx-flickering-grid';

@Component({
  selector: 'welcome-splash',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NgxThreeGlobeComponent, NgxFlickeringGridComponent],
  template: `
    <div
      class="welcome-overlay fixed inset-0 z-[9999] flex items-center justify-center overflow-hidden bg-black text-white"
    >
      <om-flickering-grid
        class="grid-layer absolute inset-0 z-0"
        [squareSize]="4"
        [gridGap]="6"
        [flickerChance]="0.3"
        color="#4f46e5"
        [maxOpacity]="0.15"
      >
      </om-flickering-grid>

      <div
        class="globe-layer absolute inset-0 z-50 flex items-center justify-center pointer-events-none mix-blend-screen opacity-80"
      >
        <div class="relative w-[150vw] h-[150vw] sm:w-[900px] sm:h-[900px]">
          <om-three-globe
            globeSize="100%"
            [globeConfig]="{
              globeColor: '#050505',
              showAtmosphere: true,
              atmosphereColor: '#4f46e5',
              atmosphereAltitude: 0.15,
              polygonColor: '#171717',
              emissive: '#000000',
              ambientLight: '#ffffff',
              directionalLeftLight: '#ffffff',
              directionalTopLight: '#ffffff',
              pointLight: '#ffffff',
              autoRotate: true,
              autoRotateSpeed: 1.2,
              arcTime: 1000,
              arcLength: 0.5,
              rings: 2,
              maxRings: 4,
            }"
            [arcAndRingColors]="['#06b6d4', '#d946ef', '#10b981']"
            styleClass="z-50"
          >
          </om-three-globe>
          <div
            class="absolute inset-0 bg-radial-gradient from-transparent via-black/40 to-black"
          ></div>
        </div>
      </div>

      <div
        class="relative z-20 flex h-full w-full flex-col items-center justify-center px-6 text-center"
      >
        <h1
          class="welcome-title text-8xl font-black uppercase tracking-tighter text-transparent drop-shadow-[0_0_20px_rgba(0,0,0,0.8)] sm:text-9xl bg-gradient-to-b from-white via-neutral-300 to-neutral-700 bg-clip-text"
        >
          Trading<br />Glossary
        </h1>
      </div>
    </div>
  `,
})
export class WelcomeSplashComponent {}
