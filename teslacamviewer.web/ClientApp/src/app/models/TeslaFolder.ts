import { TeslaClipGroup } from "./TeslaClipGroup";
import { TeslaEvent } from "./TeslaEvent";

export class TeslaFolder {
    name: string;
    actualPath: string;
    thumbnail: boolean;
    teslaEvent: TeslaEvent;
    TeslaClipGroups: TeslaClipGroup[];
    folderType: string;
}