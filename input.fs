\ input.fs -- Input sources and stream

\ Copyright 2011 (C) David Vazquez

\ This file is part of Eulex.

\ Eulex is free software: you can redistribute it and/or modify
\ it under the terms of the GNU General Public License as published by
\ the Free Software Foundation, either version 3 of the License, or
\ (at your option) any later version.

\ Eulex is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
\ GNU General Public License for more details.

\ You should have received a copy of the GNU General Public License
\ along with Eulex.  If not, see <http://www.gnu.org/licenses/>.

require @linedit.fs

' input_buffer_in alias >IN

: source-id ( -- n )
    input_source_id @ ;

: source ( -- addr n )
    input_buffer @
    input_buffer_size @ ;

: save-input
    input_buffer @
    input_buffer_in @
    input_buffer_size @
    input_source_line @
    input_source_column @
    5 ;

: restore-input
    drop
    input_source_column !
    input_source_line !
    input_buffer_size !
    input_buffer_in !
    input_buffer ! ;

CREATE TIB video-width allot

: interactive? source-id 0= ;

: refill ( -- flag )
    interactive? if
        TIB video-width accept space
        input_buffer_size !
        TIB input_buffer !
        >IN 0!
        true
    else
        false
    endif
;
:noname
    space
    state @ if
        ." compiled" cr
    else
        ." ok" cr
    then
    refill drop
; tib_fill_routine !

: query
    input_source_id 0!
    refill drop ;

\ input.fs ends here