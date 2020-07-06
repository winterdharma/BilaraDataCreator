# BilaraDataCreator

This script was created to automate the generation of Bilara JSON data files for SuttaCentral's Bilara project. Two input text files are required to create the five data file types possible (root, reference, variant, html, and translation). This ConsoleApp is designed to handle Taisho primary texts as published at [SAT 2018 DB](https://21dzk.l.u-tokyo.ac.jp/SAT2018/master30.php?lang=en) and English translations saved as plain text. Those files are lightly marked up to provide meta data, paragraph and segment breaks, and textual variant readings.

An example Taisho text is shown below to illustrate the markup conventions required by the application.

The first six lines list meta data: 
1. Agama reference to be used in Bilara
1. the Taisho text number
1. the original Taisho sutra number
1. Chinese title of the Taisho text
1. Chinese title of Agama collection's division
1. Chinese title of the Sutra

Note that line headers should be included as formatted by SAT as below (for example, "T0099_.02.0206a14:").
Paragraph ending are marked with "//" and segment endings are marked with "/".
Textual variants are appended to the end of the segment in which they occur and placed inside square brackets ("[]").
The formatting of variant readings is according to Bilara standards.
```
sa14.3
t99
803
雜阿含經
卷第二十九
(八〇三)
T0099_.02.0206a14: 如是我聞。/一時佛住舍衞國祇樹給
T0099_.02.0206a15: 孤獨園。//爾時世尊告諸比丘。/修習安那般那
T0099_.02.0206a16: 念。/若比丘修習安那般那念。/多修習者。得
T0099_.02.0206a17: 身心止息。有覺有觀。寂滅純一明分想修習
T0099_.02.0206a18: 滿足。[分想 → 分 (聖)]//何等爲修習安那般那念。多修習已。
T0099_.02.0206a19: 身心止息。有覺有觀。寂滅純一明分想。修習
T0099_.02.0206a20: 滿足。/是比丘。若依聚落城邑止住。/晨朝著
T0099_.02.0206a21: 衣持鉢。入村乞食。/善護其身。守諸根門。善
T0099_.02.0206a22: 繋心住。//乞食已還住處。/擧衣鉢洗足已。或
T0099_.02.0206a23: 入林中閑房樹下。或空露地。/端身正坐。繋
T0099_.02.0206a24: 念面前。/斷世貪愛。離欲清淨。/瞋恚睡眠掉
T0099_.02.0206a25: 悔疑斷。/度諸疑惑。於諸善法。心得決定。//遠
T0099_.02.0206a26: 離五蓋煩惱於心令慧力羸爲障礙分不
T0099_.02.0206a27: 趣涅槃。[慧 → 恚 (明)]/念於内息繋念善學。/念於外息
T0099_.02.0206a28: 繋念善學。//息長息短覺知一切身入息。於
T0099_.02.0206a29: 一切身入息善學/覺知一切身出息。於一切
T0099_.02.0206b01: 身出息善學/覺知一切身行息入息。於一切
T0099_.02.0206b02: 身行息入息善學/覺知一切身行息出息。於
T0099_.02.0206b03: 一切身行息出息善學[切(2nd) → 心 (宋, 元, 明, 聖, 大)]//覺知喜覺知樂覺
T0099_.02.0206b04: 知心行。覺知心行息入息。於覺知心行息
T0099_.02.0206b05: 入息善學[喜 → 善 (宋, 元) | 心(1st) → 身 (宋, 元, 明, 聖, 大)]/覺知心行息出息。於覺知心行
T0099_.02.0206b06: 息出息。善學//覺知心覺知心悦覺知心定。
T0099_.02.0206b07: 覺知心解脱入息。於覺知心解脱入息善
T0099_.02.0206b08: 學/覺知心解脱出息。於覺知心解脱出息
T0099_.02.0206b09: 善學//觀察無常。觀察斷。觀察無欲。觀察滅
T0099_.02.0206b10: 入息。於觀察滅入息善學。/觀察滅出息。於
T0099_.02.0206b11: 觀察滅出息善學。//是名修安那般那念。身
T0099_.02.0206b12: 止息心止息。有覺有觀。寂滅純一明分想修
T0099_.02.0206b13: 習滿足。//佛説此經已。諸比丘聞佛所説。歡
T0099_.02.0206b14: 喜奉行
```
