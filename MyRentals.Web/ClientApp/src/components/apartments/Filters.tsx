import { h, FunctionComponent, Fragment } from "preact";
import { Label, Input, Field, Control } from "rbx";

import { Filters } from "../../types/models";
import { OnChange } from "../../types/inputs";

interface Props {
    filters: Filters;
    setValue: (k: Partial<Filters>) => void;
}

const nullIfEmpty = (s: string): string | number =>
    s === "" ? "" : parseInt(s);
const FiltersComp: FunctionComponent<Props> = ({ filters, setValue }) => (
    <Fragment>
        <Field>
            <Label className="col-2 col-form-label">Size</Label>
            <Control>
                <Input
                    value={filters.floorArea}
                    type="number"
                    onChange={(e: OnChange): void =>
                        setValue({ floorArea: nullIfEmpty(e.target.value) })
                    }
                />
            </Control>
        </Field>
        <Field>
            <Label className="col-2 col-form-label">Price</Label>
            <Control>
                <Input
                    value={filters.pricePerMonth}
                    type="number"
                    onChange={(e: OnChange): void =>
                        setValue({ pricePerMonth: nullIfEmpty(e.target.value) })
                    }
                />
            </Control>
        </Field>
        <Field>
            <Label className="col-2 col-form-label">Rooms</Label>
            <Control>
                <Input
                    value={filters.numberOfRooms}
                    type="number"
                    onChange={(e: OnChange): void =>
                        setValue({ numberOfRooms: nullIfEmpty(e.target.value) })
                    }
                />
            </Control>
        </Field>
    </Fragment>
);

export default FiltersComp;
