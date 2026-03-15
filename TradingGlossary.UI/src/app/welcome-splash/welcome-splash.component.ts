import {
  ChangeDetectionStrategy,
  Component,
} from '@angular/core';
import { NgxThreeGlobeComponent } from '@omnedia/ngx-three-globe';
import { NgxFlickeringGridComponent } from '@omnedia/ngx-flickering-grid';

@Component({
  selector: 'welcome-splash',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NgxThreeGlobeComponent, NgxFlickeringGridComponent],
  template: `
    <div class="w-screen h-screen bg-black">
      <om-flickering-grid
        [squareSize]="6"
        [gridGap]="8"
        [flickerChance]="0.5"
        [color]="'#ffcc00'"
        [maxOpacity]="0.1"

      >
        <om-three-globe
          [globeConfig]="{
      globeColor: '#11263f',
      autoRotateSpeed: 0.8,
    }"
          [arcAndRingColors]="['#ff6b6b', '#f06595', '#faa2c1']">
        </om-three-globe>
      </om-flickering-grid>
    </div>

  `,
})
export class WelcomeSplashComponent {

}
